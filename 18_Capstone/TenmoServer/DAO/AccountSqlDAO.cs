﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO : IAccountDAO
    {
        private readonly string connectionString;

        public AccountSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public void AddToBalance(int userId, decimal adjustment)
        {
            decimal currentBalance = GetAccount(userId).Balance;
            currentBalance += adjustment;
            int accountId = GetAccount(userId).AccountID;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE accounts SET balance = @currentBalance WHERE account_Id = @accountId", conn);
                    cmd.Parameters.AddWithValue("@currentBalance", currentBalance);
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

        }

        public Account GetAccount(int userId)
        {
            Account returnAccount = null;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM accounts WHERE user_id = @userId", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnAccount = GetAccountFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return returnAccount;
        }


        public decimal GetBalance(int userId)
        {
            return GetAccount(userId).Balance;
        }

        public void RemoveFromBalance(int userId, decimal adjustment)
        {
            decimal currentBalance = GetAccount(userId).Balance;
            currentBalance -= adjustment;
            int accountId = GetAccount(userId).AccountID;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE accounts SET balance = @currentBalance WHERE account_Id = @accountId", conn);
                    cmd.Parameters.AddWithValue("@currentBalance", currentBalance);
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
         
        }        
        private Account GetAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account()
            {
                AccountID = Convert.ToInt32(reader["account_id"]),
                UserID = Convert.ToInt32(reader["user_id"]),
                Balance = Convert.ToDecimal(reader["balance"]),
               
            };

            return account;
        }
    }
}
