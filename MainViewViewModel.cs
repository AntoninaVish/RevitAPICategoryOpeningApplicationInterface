using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using RevitAPICategoryOpeningLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPICategoryOpeningApplicationInterface
{
    public class MainViewViewModel
    {

        private ExternalCommandData _commandData;

        private Document _doc;

        public List<ViewPlan> Views { get; }

        public List<Category> Categories { get; }

        public DelegateCommand HideCommand { get; }

        public DelegateCommand TempHideCommand { get; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            _doc = _commandData.Application.ActiveUIDocument.Document;

            Views = ViewsUtils.GetFloorPlanViews(_doc);

            Categories = CategoryUtils.GetCategories(_doc);

            HideCommand = new DelegateCommand(OnHideCommand);

            TempHideCommand = new DelegateCommand(OnTempHideCommand);
        }

        private void OnTempHideCommand()
        {
            if (SelectedViewPlan == null || SelectedCategory == null) //проверяем выбранный вид и выбранную категорию
                return;

            using (var ts = new Transaction(_doc, "Save changes"))//логика для временного скрытия данной категории
            {
                ts.Start();
                SelectedViewPlan.HideCategoryTemporary(SelectedCategory.Id);

                ts.Commit();
            }

            RaiseCloseRequest();
        }

        private void OnHideCommand() //на выбраном плате скрываем выбранную категорию
        {
            if (SelectedViewPlan == null || SelectedCategory == null)
                return;


            using (var ts = new Transaction(_doc, "Save changes"))
            {
                ts.Start();
                SelectedViewPlan.SetCategoryHidden(SelectedCategory.Id, hide: true); //вносим изменения, которые хотим увидеть в моделе

                ts.Commit();
            }

            RaiseCloseRequest();
        }

        public ViewPlan SelectedViewPlan { get; set; }

        public  Category SelectedCategory { get; set; }

        public event EventHandler CloseRequest;

        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

    }
}
