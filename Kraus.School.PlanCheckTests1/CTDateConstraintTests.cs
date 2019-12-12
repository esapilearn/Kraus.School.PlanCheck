using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kraus.School.PlanCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESAPIX.Facade.API;
using ESAPIX.Constraints;

namespace Kraus.School.PlanCheck.Tests
{
    [TestClass()]
    public class CTDateConstraintTests
    {
        [TestMethod()]
        public void ConstrainTestPassesTest()
        {
            var im = new Image();
            im.CreationDateTime = DateTime.Now.AddDays(-59);
            var expected = ResultType.PASSED;
            var actual = new CTDateConstraint().Constrain(im).ResultType;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConstrainTestFailsTest()
        {
            var im = new Image();
            im.CreationDateTime = DateTime.Now.AddDays(-61);
            var expected = ResultType.ACTION_LEVEL_3;
            var actual = new CTDateConstraint().Constrain(im).ResultType;
            Assert.AreEqual(expected, actual);

        }
    }
}