using LibertyWebAPI.DTO.FOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.BusinessServices.Contracts
{
    public interface IFOAAccountValidationService
    {
        void ValidateFOAAccount(FOAAccountValidationRequestDTO request, out int resultCode, out string message);
    }
}
