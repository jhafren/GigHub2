﻿using GigHub2.Core;
using GigHub2.Core.Models;
using GigHub2.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub2.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Mine()
        {
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(User.Identity.GetUserId());
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Gig"
            };
            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel()
            {
                Id = gig.Id,
                Heading = "Edit a Gig",
                Genres = _unitOfWork.Genres.GetGenres(),
                Date = gig.DateTime.ToString("d.M.yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel() { Gig = gig };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                viewModel.IsAttending =
                    _unitOfWork.Attendances.GetAttendance(userId, gig.Id) != null;

                viewModel.IsFollowing =
                    _unitOfWork.Followings.GetFollowing(gig.ArtistId, userId) != null;
            }

            return View("GigDetails", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();


            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.Venue, viewModel.GetDateTime(), viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}