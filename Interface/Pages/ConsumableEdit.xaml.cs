using Common;
using Logic;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Interface.Pages
{
    /// <summary>
    /// Логика взаимодействия для ConsumableEdit.xaml
    /// </summary>
    public partial class ConsumableEdit : Page
    {
        private Consumable _consumable;
        public ConsumableEdit()
        {
            InitializeComponent();
            InitializeUI();
            InitializeButtons();
        }

        public ConsumableEdit(Consumable consumable)
        {
            InitializeComponent();
            _consumable = consumable;
            InitializeUI();
            InitializeButtons();
        }

        private void InitializeUI()
        {
            if (_consumable != null)
            {
                Consumable responsibleUser = GetEntity<Consumable>(_consumable.ResponsibleUserId);
                Consumable temporarilyResponsibleUser = GetEntity<Consumable>(_consumable.TemporarilyResponsibleUserId);
                Consumable consumableType = GetEntity<Consumable>(_consumable.ConsumableTypeId);

                PopulateUserComboBoxes();
                TextBoxName.Text = _consumable.Name;
                TextBoxDescription.Text = _consumable.Description;
                TextBoxReceiptDate.Text = _consumable.ReceiptDate;
                TextBoxPhoto.Text = _consumable.Photo;
                TextBoxCount.Text = _consumable.Count.ToString();
                ComboBoxResponsibleUser.SelectedItem = responsibleUser.Name;
                ComboBoxTemporarilyResponsibleUser.SelectedItem = temporarilyResponsibleUser.Name;
                ComboBoxConsumableType.SelectedItem = consumableType.Name;
            }
        }

        private void InitializeButtons()
        {
            ButtonAdd.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Add);
            ButtonUpdate.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Update);
            ButtonDelete.Click += (_, __) => ModifyAudience(DatabaseModify.Action.Delete);
        }

        private void PopulateUserComboBoxes()
        {
            var consumable = GetEntityList<Consumable>("Consumable");
            ComboBoxResponsibleUser.ItemsSource = consumable.Select(c => c.Name);
            ComboBoxTemporarilyResponsibleUser.ItemsSource = consumable.Select(c => c.Name);
            ComboBoxConsumableType.ItemsSource = consumable.Select(c => c.Name);
        }

        private void ModifyAudience(DatabaseModify.Action action)
        {
            int responsibleUserId = GetIdFromComboBox(ComboBoxResponsibleUser);
            int temporarilyResponsibleUserId = GetIdFromComboBox(ComboBoxTemporarilyResponsibleUser);
            int consumableTypeId = GetIdFromComboBox(ComboBoxConsumableType);

            if (responsibleUserId == 0 || temporarilyResponsibleUserId == 0 || consumableTypeId == 0)
            {
                TextBlockError.Text = "Empty value";
                TextBlockError.Visibility = System.Windows.Visibility.Visible;
            }

            var consumables = _consumable ?? new Consumable();

            consumables.Name = TextBoxName.Text;
            consumables.Description = TextBoxDescription.Text;
            consumables.ReceiptDate = TextBoxReceiptDate.Text;
            consumables.Photo = TextBoxReceiptDate.Text;
            consumables.Count = int.Parse(TextBoxCount.Text);
            consumables.ResponsibleUserId = responsibleUserId;
            consumables.TemporarilyResponsibleUserId = temporarilyResponsibleUserId;
            consumables.ConsumableTypeId = consumableTypeId;

            var (result, error) = DatabaseModify.ModifyEntity(consumables, action);

            TextBlockError.Text = result ? "" : error;
            TextBlockError.Visibility = result ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        private int GetIdFromComboBox(ComboBox comboBox)
        {
            string selectedName = comboBox.SelectedItem as string;
            var consumableCharacteristics = GetEntityList<ConsumableCharacteristics>("ConsumableCharacteristics");
            var consumableCharacteristic = consumableCharacteristics.FirstOrDefault(u => u.Name == selectedName);
            return consumableCharacteristic != null ? consumableCharacteristic.Id : 0;
        }

        private TEntity GetEntity<TEntity>(int id) where TEntity : class
        {
            return DatabaseReader.GetEntity<TEntity>(id);
        }

        private List<TEntity> GetEntityList<TEntity>(string entityName) where TEntity : class
        {
            return DatabaseReader.GetEntityList(entityName).OfType<TEntity>().ToList();
        }
    }
}