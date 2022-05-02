﻿using _0_Framework.Application;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System.Collections.Generic;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            //if (_slideRepository.Exist(x => x.Heading == command.Heading))
            //    return operation.Failed(ApplicationMessages.DoublicatedRecord);

            var slide = new Slide(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading, command.Title, command.Text, command.BtnText);
            _slideRepository.Create(slide);
            _slideRepository.Savechanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(command.Id);
            if (slide == null) return operation.Failed(ApplicationMessages.RecordNotFound);

           // if (_slideRepository.Exist(x => x.Heading == command.Heading)) return operation.Failed(ApplicationMessages.DoublicatedRecord);

            slide.Edit(command.Picture, command.PictureAlt, command.PictureTitle, command.Heading, command.Title, command.Text, command.BtnText);
            _slideRepository.Savechanges();
            return operation.Succedded();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null) return operation.Failed(ApplicationMessages.RecordNotFound);
            slide.Remove();
            _slideRepository.Savechanges();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = _slideRepository.Get(id);
            if (slide == null) return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Restore();
            _slideRepository.Savechanges();
            return operation.Succedded();
        }
    }
}
