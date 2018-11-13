using LibertyWebAPI.DTO.Common;
using LibertyWebAPI.DTO.FOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibertyWebAPI.DataModel.Contracts
{
    public interface IFOAAccountValidationRepository
    {
        void ValidateFOAAccount(FOAAccountValidationRequestDTO request, out int resultCode, out string message);
    }
}
