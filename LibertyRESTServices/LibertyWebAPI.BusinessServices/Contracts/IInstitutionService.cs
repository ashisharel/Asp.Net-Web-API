using LibertyWebAPI.BusinessEntities;
using LibertyWebAPI.DTO.Institution;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface IInstitutionService
    {
        InstitutionResponseDTO GetInstitution(InstitutionRequestDTO institutionRequest);
        InstitutionDTO MapDTO(Institution intitution);
    }
}
