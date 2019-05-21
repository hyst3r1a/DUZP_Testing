using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DUZP_Testing
{
    [Serializable]
    public class TestSuite
    {
        public List<Question> tests = new List<Question>();
        public String testName;
        public String maxPts;

    }
}
