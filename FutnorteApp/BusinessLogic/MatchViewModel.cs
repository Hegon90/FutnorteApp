using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutnorteApp.BusinessLogic
{
    internal class MatchViewModel
    {
        private readonly MatchService _matchService;
        public MatchViewModel(MatchService matchService)
        {
            _matchService = matchService;
        }


    }
}
