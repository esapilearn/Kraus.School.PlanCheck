using ESAPIX.Common;
using ESAPIX.Constraints.DVH;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using VMS.TPS.Common.Model.API;

namespace Kraus.School.PlanCheck.ViewModels
{
    public class MainViewModel : BindableBase
    {
        AppComThread VMS = AppComThread.Instance;

        public MainViewModel()
        {
            CreateConstraints();

            EvaluateCommand = new DelegateCommand(() =>
            {
                foreach (var pc in Constraints)
                {
                    var result = VMS.GetValue(sc =>
                    {
                        var canConstrain = pc.Constraint.CanConstrain(sc.PlanSetup);
                        if (!canConstrain.IsSuccess)
                        {
                            return canConstrain;
                        }
                        else
                        {
                            return pc.Constraint.Constrain(sc.PlanSetup);
                        }
                    });

                    pc.Result = result;
                }
            });

            //OnPlanChanged(VMS.GetValue(sac => sac.PlanSetup));

            //VMS.Execute(sac => sac.PlanSetupChanged += OnPlanChanged);
        }
        /*
        public void OnPlanChanged(PlanSetup ps)
        {
            VMS.Invoke(() =>
            {
                EvaluateCommand = new DelegateCommand(() =>
                {
                    foreach (var pc in Constraints)
                    {
                        var result = VMS.GetValue(sc =>
                        {
                            var canConstrain = pc.Constraint.CanConstrain(sc.PlanSetup);
                            if (!canConstrain.IsSuccess)
                            {
                                return canConstrain;
                            }
                            else
                            {
                                return pc.Constraint.Constrain(sc.PlanSetup);
                            }
                        });

                        pc.Result = result;
                    }
                });
            });
        }
        */
        private void CreateConstraints()
        {
            Constraints.AddRange(new PlanConstraint[]
            {
                    new PlanConstraint(ConstraintBuilder.Build("PTV", "Max[%] <= 110")),
                    new PlanConstraint(ConstraintBuilder.Build("Rectum", "V75Gy[%] <= 15")),
                    new PlanConstraint(ConstraintBuilder.Build("Rectum", "V65Gy[%] <= 35")),
                    new PlanConstraint(ConstraintBuilder.Build("Bladder", "V80Gy[%] <= 15")),
                    new PlanConstraint(new CTDateConstraint())
            });
        }

        public ObservableCollection<PlanConstraint> Constraints { get; set; } = new ObservableCollection<PlanConstraint>();
        public DelegateCommand EvaluateCommand { get; set; }


    }
}
