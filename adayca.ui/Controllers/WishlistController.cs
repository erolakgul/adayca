using adayca.ui.BaseInterface;
using adayca.ui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adayca.ui.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ILogger<WishlistController> _logger;
        private readonly ICustomOptions<CustomConfiguration> _customOptions;
        private readonly ICustomMemoryCache _customMemoryCache;
        private readonly ICustomVariables _customVariable;
        private readonly ICustomHttpClient _customHttpClient;

        private readonly string uri = String.Empty;
        private readonly Guid _generateGuid;


        public WishlistController(ILoggerFactory loggerFactory, ICustomOptions<CustomConfiguration> customOptions
                    , ICustomMemoryCache customMemoryCache
            , ICustomVariables customVariables
            , ICustomHttpClient customHttpClient)
        {
            _logger = loggerFactory.CreateLogger<WishlistController>();
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

            #region logging
            _customVariable.SetNlogVariable("UserId_From_Ct", "EAKGUL_" + _generateGuid);

            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
            _logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, CustomLogEvents.GetPage, _generateGuid + " Page {Page} visited at {Runtime} with this ip : {ip}", HttpContext.Request.Path.Value, DateTime.Now, remoteIpAddress);

            #endregion

            return View();
        }
    }
}
