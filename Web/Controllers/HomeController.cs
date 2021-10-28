using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortfolioItem> _portfolioItems;

        public HomeController(IUnitOfWork<Owner>owner,IUnitOfWork<PortfolioItem>portfolioItems)
        {
            _owner = owner;
            _portfolioItems = portfolioItems;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().First(),
                PortfolioItems = _portfolioItems.Entity.GetAll().ToList()
            };

            return View(homeViewModel);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
