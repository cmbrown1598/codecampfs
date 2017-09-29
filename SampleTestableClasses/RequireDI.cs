using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTestableClasses
{
    public class RequireDi
    {
        private readonly IDoStuff _stuffDoer;

        public RequireDi(IDoStuff stuffDoer)
        {
            _stuffDoer = stuffDoer;
        }

        public int MethodThatRequiresDi(int x)
        {
            var y = _stuffDoer.DoStuffForRealsies(5, x, new Item {Name = Constants.Sulfuras, Quality = 4, SellIn = 10});
            return y / 2;
        }
    }
}
