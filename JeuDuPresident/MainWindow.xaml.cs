using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JeuDuPresident
{
    using President.ObjectModel;
    using System.Security.Policy;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            var playerList = new List<Player>
                            {
                                new Bot("Joueur 1", Order.Top),
                                new Bot("Joueur 2", Order.Left),
                                new Bot("Joueur 3", Order.Bottom), 
                                new Bot("Joueur 4", Order.Right) 
                            };

            var game = new Game(playerList, 2);

            game.InitializeRound();
            this.RenderCards(game.Players);
        }

        private void RenderCards(List<Player> players)
        {
            foreach (var player in players)
            {
                var panelToAdd = CardGrid.FindName(player.Order + "Panel") as WrapPanel;
                foreach (var card in player.PlayerCards.SelectMany(p => p.Cards))
                {
                    var img = new Image();
                    var source = new BitmapImage();

                    source.BeginInit();
                    source.UriSource = new Uri("pack://application:,,,/assets/img/cards/" + card + ".png");

                    if (panelToAdd.Name == "LeftPanel" || panelToAdd.Name == "RightPanel")
                    {
                        source.Rotation = Rotation.Rotate90;
                        img.Width = 80;
                        img.Margin = new Thickness(0, 0, 0, -35);
                    }
                    else
                    {
                        img.Width = 60;
                        img.Margin = new Thickness(0, 0, -28, 0);
                    }
                    source.EndInit();

                    img.Source = source;

                    panelToAdd.Children.Add(img);
                }
            }
        }
    }
}
