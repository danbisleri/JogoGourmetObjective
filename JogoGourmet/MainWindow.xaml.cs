using JogoGourmet.Model;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace JogoGourmet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        private readonly List<Food> foodList;
        private bool response;

        public MainWindow()
        {
            InitializeComponent();

            foodList = new List<Food>();
            foodList.Add(new Food("Lasanha", "Massa"));
            foodList.Add(new Food("Bolo de Chocolate", ""));

            label.Content = "Pense em um comida que vc gosta.";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void Start()
        {
            List<Food> foods = new List<Food>();
            string lastFood = string.Empty;
            foods = foodList;
            MessageDialogResult result;
            bool accept = false;

            var settingsYesNo = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No"
            };

            var settingsOK = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK"
            };

            foreach (var food in foods)
            {
                if (!string.IsNullOrEmpty(food.Feature))
                {
                    result = DialogManager.ShowModalMessageExternal(this, "", "O prato que você pensou é " + food.Feature, MessageDialogStyle.AffirmativeAndNegative, settings: settingsYesNo);
                    
                    if (result == MessageDialogResult.Affirmative)
                    {
                        foreach (var foodFeature in foods.Where(f => f.Feature == food.Feature))
                        {
                            result = DialogManager.ShowModalMessageExternal(this, "", "O prato que você pensou é " + foodFeature.Name + " ?", MessageDialogStyle.AffirmativeAndNegative, settings: settingsYesNo);

                            if (result == MessageDialogResult.Affirmative)
                            {
                                DialogManager.ShowModalMessageExternal(this, "", "Acertei!", settings: settingsOK);
                                accept = true;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }

                        if (!accept)
                        {
                            var name = DialogManager.ShowModalInputExternal(this, "", "Qual prato você pensou?",settingsOK);

                            foodList.Add(new Food(name, food.Feature));
                            accept = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        lastFood = food.Name;
                        continue;
                    }
                }
                else
                {
                    result = DialogManager.ShowModalMessageExternal(this, "", "O prato que você pensou é " + food.Name + " ?", MessageDialogStyle.AffirmativeAndNegative, settings: settingsYesNo);
                    lastFood = food.Name;
                    if (result == MessageDialogResult.Affirmative)
                    {
                        DialogManager.ShowModalMessageExternal(this, "", "Acertei!", settings: settingsOK);
                        accept = true;
                        break;
                    }
                }
            }

            if (!accept)
            {
                var name = DialogManager.ShowModalInputExternal(this, "", "Qual prato você pensou?", settingsOK);

                var feature = DialogManager.ShowModalInputExternal(this, "", "O que o " + name + " é que o " + lastFood + " não é ?", settingsOK);

                foodList.Add(new Food(name, feature));
            }


        }


    }
}
