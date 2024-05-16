using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace MVC.Controllers.Bases
{
    #region Localization
    public abstract class MvcControllerBase : Controller 
    {
        protected MvcControllerBase()
        {
            CultureInfo culture = new CultureInfo("en-US"); 
                                                            
                                                            
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
    #endregion
}