using AutoMapper;
using MonefyWeb.Application.ModelsWebPage.Models;
using MonefyWeb.ApplicationServices.ApplicationWebPage.Contracts;
using MonefyWeb.DomainServices.DomainWebPage.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyWeb.ApplicationServices.ApplicationWebPage.Implementations
{
    public class CryptoAppService : ICryptoAppService
    {
        private readonly ICryptoLogic _cryptoLogic;
        private readonly IMapper _mapper;

        public CryptoAppService(ICryptoLogic cryptoLogic, IMapper mapper)
        {
            _cryptoLogic = cryptoLogic;
            _mapper = mapper;
        }

        public async Task<List<CryptoDataResponse>> GetCryptoData()
        {
            var cryptoData = await _cryptoLogic.GetCryptoData();

            return _mapper.Map<List<CryptoDataResponse>>(cryptoData);
        }
    }
}
