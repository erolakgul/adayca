using adayca.ui.BaseInterface;
using adayca.ui.Models;
using adayca.ui.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace adayca.ui.Controllers  
{ 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomOptions<CustomConfiguration> _customOptions;
        private readonly ICustomMemoryCache _customMemoryCache;
        private readonly ICustomVariables _customVariable;
        private readonly ICustomHttpClient _customHttpClient;

        private readonly string uri = String.Empty;
        private readonly Guid _generateGuid;
          
        public HomeController(ILoggerFactory loggerFactory, ICustomOptions<CustomConfiguration> customOptions
                    , ICustomMemoryCache customMemoryCache
            , ICustomVariables customVariables
            , ICustomHttpClient customHttpClient)
        {
            _logger = loggerFactory.CreateLogger<HomeController>();
            _customOptions = customOptions;
            _customMemoryCache = customMemoryCache;
            _customVariable = customVariables;
            _customHttpClient = customHttpClient;

            #region api url
            uri = _customOptions.GetUrl("ERP_PRIORITY_URL").url;
            #endregion

            #region variables
             _generateGuid = _customVariable.GenerateGuid();
            #endregion
        }

        public IActionResult Index()
        {
            #region api request
            //_customHttpClient.Set().GetAsync("uri"); 
            #endregion

            #region caching
            _customMemoryCache.Set("x", "x");
            var value = _customMemoryCache.Get("x");
            _customMemoryCache.Remove("x");
            var valueCntrl = _customMemoryCache.Get("x");
            #endregion


            #region appsettings file parameters
            _customOptions.Update(opt =>
              {
                  opt.path = "Logs/EAKGUL_.log";
              });
            #endregion

            #region logging
            _customVariable.SetNlogVariable("UserId_From_Ct", "EAKGUL_" + _generateGuid);

            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            _logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, CustomLogEvents.GetPage, _generateGuid + " Page {Page} visited at {Runtime} with this ip : {ip}", HttpContext.Request.Path.Value, DateTime.Now, remoteIpAddress);

            #endregion 


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
