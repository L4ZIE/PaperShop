using PaperShop.BackPaper.DataAccess.Models;
using PaperShop.BackPaper.DataAccess.RepoInterfaces;
using PaperShop.BackPaper.Services.DTO.Requests;
using PaperShop.BackPaper.Services.DTO.Responses;

namespace PaperShop.BackPaper.Services.Service
{
    public class PropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public List<PropertyDto> GetAllProperties()
        {
            return _propertyRepository.GetAllProperties().Select(PropertyDto.FromEntity).ToList();
        }

        public PropertyDto? GetPropertyById(int id)
        {
            var property = _propertyRepository.GetById(id);
            return property == null ? null : PropertyDto.FromEntity(property);
        }

        public Property CreateProperty(CreatePropertyDto dto)
        {
            var property = new Property
            {
                PropertyName = dto.PropertyName
            };
            return _propertyRepository.Add(property);
        }

        public void UpdateProperty(UpdatePropertyDto dto, int id)
        {
            var property = new Property
            {
                Id = id,
                PropertyName = dto.PropertyName ?? string.Empty 
            };
            _propertyRepository.Update(property);
        }

        public void DeleteProperty(int id)
        {
            _propertyRepository.Delete(id);
        }
    }
}