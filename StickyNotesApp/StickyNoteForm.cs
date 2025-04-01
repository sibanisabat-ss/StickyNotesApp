using System;
using System.Drawing;
using System.Windows.Forms;

namespace StickyNotesApp
{
    public partial class StickyNoteForm: Form
    {
        private TextBox titleTextBox;
        private TextBox contentTextBox;
        private ComboBox colorComboBox;

        public string NoteTitle { get; private set; }
        public string NoteContent { get; private set; }
        public Color NoteColor { get; private set; }

        public StickyNoteForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "New Note";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label titleLabel = new Label
            {
                Text = "Title:",
                Location = new Point(10, 15),
                Size = new Size(50, 20)
            };

            titleTextBox = new TextBox
            {
                Location = new Point(70, 12),
                Size = new Size(300, 20)
            };

            Label contentLabel = new Label
            {
                Text = "Content:",
                Location = new Point(10, 45),
                Size = new Size(60, 20)
            };

            contentTextBox = new TextBox
            {
                Location = new Point(10, 70),
                Size = new Size(360, 180),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            Label colorLabel = new Label
            {
                Text = "Color:",
                Location = new Point(10, 260),
                Size = new Size(50, 20)
            };

            colorComboBox = new ComboBox
            {
                Location = new Point(70, 257),
                Size = new Size(120, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Add colors to the ComboBox
            colorComboBox.Items.Add("Yellow");
            colorComboBox.Items.Add("Green");
            colorComboBox.Items.Add("Pink");
            colorComboBox.Items.Add("Blue");
            colorComboBox.Items.Add("Black");
            colorComboBox.Items.Add("Red");
            colorComboBox.Items.Add("Purple");
            colorComboBox.Items.Add("Brown");
            colorComboBox.Items.Add("Orange");
            colorComboBox.SelectedIndex = 0;

            Button saveButton = new Button
            {
                Text = "Save",
                Location = new Point(200, 270),
                Size = new Size(80, 30),
                DialogResult = DialogResult.OK
            };
            saveButton.Click += SaveButton_Click;

            Button cancelButton = new Button
            {
                Text = "Cancel",
                Location = new Point(290, 270),
                Size = new Size(80, 30),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(titleLabel);
            this.Controls.Add(titleTextBox);
            this.Controls.Add(contentLabel);
            this.Controls.Add(contentTextBox);
            this.Controls.Add(colorLabel);
            this.Controls.Add(colorComboBox);
            this.Controls.Add(saveButton);
            this.Controls.Add(cancelButton);

            this.AcceptButton = saveButton;
            this.CancelButton = cancelButton;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                MessageBox.Show("Please enter a title for the note.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }

            NoteTitle = titleTextBox.Text;
            NoteContent = contentTextBox.Text;

            switch (colorComboBox.SelectedItem.ToString())
            {
                case "Yellow":
                    NoteColor = Color.LightYellow;
                    break;
                case "Green":
                    NoteColor = Color.LightGreen;
                    break;
                case "Pink":
                    NoteColor = Color.LightPink;
                    break;
                case "Blue":
                    NoteColor = Color.LightBlue;
                    break;
                case "Black":
                    NoteColor = Color.Black;
                    break;
                case "Orange":
                    NoteColor = Color.Orange;
                    break;
                case "Brown":
                    NoteColor = Color.Brown;
                    break;
                case "Red":
                    NoteColor = Color.Red;
                    break;
                case "Purple":
                    NoteColor = Color.Purple;
                    break;
                default:
                    NoteColor = Color.LightYellow;
                    break;
            }
        }
    }
}
