using account_web.Models.Dtos;
using account_web.Services;
using Microsoft.AspNetCore.Mvc;

namespace account_web.Controllers;

public class FactoryController : BaseViewController
{
    private readonly FactoryServices _factoryServices;

    public FactoryController(FactoryServices factoryServices)
    {
        _factoryServices = factoryServices;
    }
    public async Task<IActionResult> Index()
    {
        if ( !IsUserLoggedIn())
        {
            return RedirectToLogin();
        }
        return View(await _factoryServices.GetFactories());
    }

    public async Task<IActionResult> Details(string factoryId)
    {
        if (!IsUserLoggedIn())
        {
            return RedirectToLogin();
        }
        if (string.IsNullOrEmpty(factoryId))
        {
            return NotFound();
        }
        var factory = await _factoryServices.GetFactoryByFactoryId(factoryId);
        if (factory == null)
        {
            return NotFound();
        }
        return View(factory);
    }

    public IActionResult Create()
    {
        if (!IsUserLoggedIn())
        {
            return RedirectToLogin();
        }
        return View();
    }

    public async Task<IActionResult> Edit(string factoryId)
    {
        if (!IsUserLoggedIn())
        {
            return RedirectToLogin();
        }
        if (string.IsNullOrEmpty(factoryId))
        {
            return NotFound();
        }
        var factory = await _factoryServices.GetFactoryByFactoryId(factoryId);
        if (factory == null)
        {
            return NotFound();
        }
        return View(factory);
    }

    public async Task<IActionResult> Delete(string factoryId)
    {
        if (!IsUserLoggedIn())
        {
            return RedirectToLogin();
        }
        if (string.IsNullOrEmpty(factoryId))
        {
            return NotFound();
        }
        var factory = await _factoryServices.GetFactoryByFactoryId(factoryId);
        if (factory == null)
        {
            return NotFound();
        }
        return View(factory);
    }

}