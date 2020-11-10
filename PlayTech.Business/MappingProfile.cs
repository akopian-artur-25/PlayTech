using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace PlayTech.Business
{
    public class EntityToDTOMapping : Profile
    {
        public EntityToDTOMapping()
        {
        }
    }

    public class DTOToEntityMapping : Profile
    {
        public DTOToEntityMapping()
        {
        }
    }
}
