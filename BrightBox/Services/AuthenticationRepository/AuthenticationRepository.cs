namespace BrightBox.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using BrightBox.Models;

    public sealed class AuthenticationRepository : IAuthenticationRepository
    {
        private const string CacheKey = "TokenArray";

        private const int MAX_TOKENS = 10;

        private string login;

        private string password;

        private List<string> tokenList; 
        
        public AuthenticationRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var tokens = new string[0];
                    ctx.Cache[CacheKey] = tokens;
                }
            }

            //TODO: Hardcode login and password
            login = "admin";
            password = "0000";
        }

        public bool Authentication(User user)
        {
            if (user == null || user.Login == null || user.Password == null) 
                return false;

            return user.Login.Equals(this.login, StringComparison.Ordinal) && (user.Password.Equals(this.password, StringComparison.Ordinal));
        }

        public bool CheckToken(string token)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentTokens = (string[])ctx.Cache[CacheKey];

                    return currentTokens.Contains(token);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }

        public string CreateToken()
        {
            Guid g = Guid.NewGuid();
            string guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            guidString = guidString.Replace("/", "");
            
            AddToken(guidString);

            return guidString;
        }


        private bool AddToken(string token)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((string[])ctx.Cache[CacheKey]).ToList();

                    currentData.Add(token);

                    if (currentData.Count >= MAX_TOKENS) 
                        currentData = currentData.Skip(MAX_TOKENS / 2).ToList();

                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }
    }
}