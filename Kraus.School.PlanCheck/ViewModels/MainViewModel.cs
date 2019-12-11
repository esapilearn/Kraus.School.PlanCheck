using ESAPIX.Common;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using VMS.TPS.Common.Model.API;

namespace Kraus.School.PlanCheck.ViewModels
{
    public class MainViewModel : BindableBase
    {
        AppComThread VMS = AppComThread.Instance;

        public MainViewModel()
        {

        }

        public ObservableCollection<PlanConstraint> Constraints { get; set; } = new ObservableCollection<PlanConstraint>();
        public DelegateCommand EvaluateCommand { get; set; }


    }
}
