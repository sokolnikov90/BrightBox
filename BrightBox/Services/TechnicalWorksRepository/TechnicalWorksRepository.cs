namespace BrightBox.Services
{
    using System;
    using System.Web;
    using BrightBox.Models;

    public class TechnicalWorksRepository : ITechnicalWorksRepository
    {
        private const string CacheKey = "TechnicalWorksStatus";

        public TechnicalWorksRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var status = new TechnicalWorks { Token = null, Status = 0, Datetime = null };
                    ctx.Cache[CacheKey] = status;
                }
            }
        }

        public TechnicalWorks GetTechnicalWorksStatus()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (TechnicalWorks)ctx.Cache[CacheKey];
            }

            return new TechnicalWorks { Token = null, Status = 0, Datetime = null };
        }

        public bool SaveContact(TechnicalWorks technicalWorks)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    technicalWorks.Token = null;
                    ctx.Cache[CacheKey] = technicalWorks;
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