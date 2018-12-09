﻿using System;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace MMS.DAL
{
    /// <summary>
    /// 数据连接事务的Session接口
    /// </summary>
    public interface IDbSession : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        IDbTransaction BeginTrans(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
    }

    /// <summary>
    /// 数据库连接事务的Session对象
    /// </summary>
    public class DbSession : IDbSession
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public IDbConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// 数据库事务对象
        /// </summary>
        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="conn">连接</param>
        public DbSession(IDbConnection conn)
        {
            _connection = conn;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="trans">事务</param>
        public DbSession(IDbConnection conn, IDbTransaction trans)
        {
            _connection = conn;
            _transaction = trans;
        }

        /// <summary>
        /// 开启会话
        /// </summary>
        /// <param name="isolation"></param>
        /// <returns></returns>
        public IDbTransaction BeginTrans(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            _transaction = _connection.BeginTransaction(isolation);
            return _transaction;
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
            _transaction = null;
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
            _transaction = null;
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                _connection.Close();
                _connection = null;
            }
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Session 创建类
    /// </summary>
    public class SessionFactory
    {
        /// <summary>
        /// 根据Provider类型，创建数据库连接
        /// </summary>
        /// <returns></returns>
        private static IDbConnection CreateConnectionByProvider(string connectionStr)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string providerName = conf["ConnectionString:" + connectionStr+ ":ProviderName"];
            IDbConnection conn = null;
            switch (providerName)
            {
                case "SqlServer":
                    conn = new SqlConnection(conf["ConnectionString:" + connectionStr + ":Value"]);
                    break;
                case "System.Data.MySqlClient":
                case "MySql.Data.MySqlClient":
                    //MySql
                    //conn = new MySqlConnection(connStringSetting.ConnectionString);
                    SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
                    break;
                //case "System.Data.OracleClient":
                //case "Oracle.DataAccess.Client":
                //    //Oracle
                //    conn = new OracleConnection(connStringSetting.ConnectionString);
                //    break;
                case "System.Data.Odbc":
                    //ODBC Excel Driver
                    break;
            }
            return conn;
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection CreateConnection(string connectionStr)
        {
            IDbConnection conn = CreateConnectionByProvider(connectionStr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        /// 创建数据库连接会话
        /// </summary>
        /// <returns></returns>
        public static IDbSession CreateSession(string connectionStr)
        {
            IDbConnection conn = CreateConnection(connectionStr);
            IDbSession session = new DbSession(conn);

            return session;
        }

        /// <summary>
        /// 创建数据库事务会话
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static IDbSession CreateSession(IDbConnection conn, IDbTransaction trans)
        {
            IDbSession session = new DbSession(conn, trans);
            return session;
        }
    }
}
