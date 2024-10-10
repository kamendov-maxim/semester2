using CalculatorAlgorithm;

namespace GUI
{
  /// <summary>
  /// Gui for calculator
  /// </summary>
  public class CalculatorForm : Form
  {
    private System.ComponentModel.IContainer? components = null;
    readonly Label output = new();

    private readonly List<string> buttonTexts = ["C", "±", "%", "/", "7", "8", "9",
  "*", "4", "5", "6", "-", "1", "2", "3", "+", "0", ",", "="];
    readonly Calculator inputHandler = new();

    public CalculatorForm() { InitializeComponent(); }

    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(240, 280);
      this.Text = "Calculator";
      this.FormBorderStyle = FormBorderStyle.FixedDialog;

      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.StartPosition = FormStartPosition.CenterScreen;

      Panel panel = new()
      {
        Location = new Point(5, 5),
        Size = new Size(230, 70),
        BorderStyle = BorderStyle.Fixed3D,
        BackColor = Color.White
      };
      this.Controls.Add(panel);

      output.Font = new Font("Arial", 24, FontStyle.Bold);
      output.Location = new Point(10, 10);
      output.TextAlign = ContentAlignment.MiddleRight;
      output.Text = "0";
      output.Size = new Size(215, 50);
      panel.Controls.Add(output);

      AddButtons();

      //Button button = new Button();
      //button.Left = 11;
      //button.Top = 10;
      //button.Size = new Size(100, 100);
      //button.Text = "hui";
      //this.Controls.Add(button);


      //this.ShowDialog();
    }

    private void button_Click(object sender, EventArgs e)
    {
      char currentCharacter = (sender as Button).Text[0];
      output.Text = inputHandler.NextStep(currentCharacter);
    }

    private void AddButtons()
    {
      int left = 0;
      int top = 80;
      int buttonHeight = 40;
      int buttonWidth = 60;
      int zeroButtonModifier = 0;
      int counter = 0;
      for (int i = 0; i < 5; ++i)
      {
        for (int j = 0; j < 4; ++j)
        {
          if (i == 4 && j == 0)
          {
            zeroButtonModifier = buttonWidth;
            ++j;
          }
          Button button = new()
          {
            Text = buttonTexts[counter],
            Left = left,
            Top = top,
            Size = new Size(buttonWidth + zeroButtonModifier, buttonHeight),
            Visible = true
          };
          this.Controls.Add(button);
          button.Click += button_Click;
          left += buttonWidth + zeroButtonModifier;
          zeroButtonModifier = 0;
          ++counter;
        }
        left = 0;
        top += buttonHeight;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }
  }
}

