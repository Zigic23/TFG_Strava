using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;
using System;
using System.Collections.Generic;

public abstract class BaseController : Controller
{
    public ILogger _logger;
    IOptions<ConnectionStrings> _connectionStrings;
    IOptions<KeysSettings> _keysSettings;
    public IConfiguration _configuration;
    public BaseController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IConfiguration configuration):base(){
        _logger = logger;
        _connectionStrings = connectionStrings;
        _keysSettings = keysSettings;
        _configuration = configuration;
    }
}