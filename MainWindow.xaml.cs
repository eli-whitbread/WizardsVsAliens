﻿using System;
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
using Microsoft.Surface;
using Microsoft.Surface.Core;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;

namespace WizardWarz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : SurfaceWindow
    {
        public Int32 tileSize = 64;

        public MainWindow()
        {
            InitializeComponent();

            InitializeGameBoard();
           
        }

        private void InitializeGameBoard()
        {
            //set the grid size
            Int32 rows = 12;
            Int32 cols = 12;

            GridLengthConverter myGridLengthConverter = new GridLengthConverter();
            GridLength side = (GridLength)myGridLengthConverter.ConvertFromString("Auto");

            //Setup the grid Rows and Columns
            for (int i = 0; i < cols; i++)
            {
                GameBoardGrid.ColumnDefinitions.Add(new System.Windows.Controls.ColumnDefinition());
                GameBoardGrid.ColumnDefinitions[i].Width = side;
            }
            for (int x = 0; x < rows; x++)
            {
                GameBoardGrid.RowDefinitions.Add(new System.Windows.Controls.RowDefinition());
                GameBoardGrid.RowDefinitions[x].Height = side;
            }

            //create an empty Rectangle array
            Rectangle[,] flrTiles = new Rectangle[cols, rows];

            //fill each element in the Rectangle array with a FloorTile image.
            for (int c = 0; c < cols; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    
                    flrTiles[c, r] = new Rectangle();
                    //add a wall tile if along the grid extremes
                    if (r == 0 || r == rows -1 || c == 0 || c == cols - 1)
                    {
                        flrTiles[c, r].Fill = new ImageBrush(new BitmapImage(new Uri(@".\Resources\WallTile_64x64.png", UriKind.Relative)));
                    }
                    //otherwise add a floor tile
                    else
                    {
                        flrTiles[c, r].Fill = new ImageBrush(new BitmapImage(new Uri(@".\Resources\FloorTile_64x64.png", UriKind.Relative)));
                    }
                    //
                    //inner solid and destrutable walls still required!!
                    //
                    flrTiles[c, r].Height = tileSize;
                    flrTiles[c, r].Width = tileSize;
                    Grid.SetColumn(flrTiles[c, r], c);
                    Grid.SetRow(flrTiles[c, r], r);

                    GameBoardGrid.Children.Add(flrTiles[c, r]);
                }
            }
        }

    }
}
