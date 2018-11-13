using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DTO.FOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.BusinessServices
{
    public class FOAAccountValidationService : IFOAAccountValidationService
    {

        private readonly IFOAAccountValidationRepository _foaAccountValidationRepository;
        public FOAAccountValidationService(IFOAAccountValidationRepository foaAccountValidationRepository)
        {
            _foaAccountValidationRepository = foaAccountValidationRepository;
        }        
        public void ValidateFOAAccount(FOAAccountValidationRequestDTO accountRequest, out int resultCode, out string message)
        {            
            _foaAccountValidationRepository.ValidateFOAAccount(accountRequest, out resultCode, out message);
            
        }
    }
}
