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
//using Microsoft.Surface;
//using Microsoft.Surface.Core;
//using Microsoft.Surface.Presentation.Controls;
//using Microsoft.Surface.Presentation.Input;
using System.Diagnostics;

namespace WizardWarz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AudioManager newAudioManager;
        GameWindow newgame;
        public TitleScreen title;
        MainMenu menu;
        EndGame endwindow;
        HelpScreen tutorial;

        protected static MainWindow mainWin;

        public static bool GlobalAudio1
        {
            get; set;
        }

        public MainWindow()
        {
            InitializeComponent();
            // Initialise the Audio Manager
            newAudioManager = new AudioManager();

            // Load the TitleScreen, and a click event for it to use (to remove it after)
            title = new TitleScreen();
            title.MouseDown += Title_MouseDown;
            // Add Title screen to the Main Window
            MainCanvas.Children.Add(title);

            tutorial = new HelpScreen();


            // Initialising Audio (visual pushing to the far right), Play open track.
            GlobalAudio1 = true;
            newAudioManager.audioOn = GlobalAudio1;
            newAudioManager.playTitleSound();
            Canvas.SetLeft(audioTile, 0);
            MainCanvas.Children.Add(newAudioManager);

            mainWin = this;
        }

        public void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // On click, open the player count menu.
            menu = new MainMenu();
            MainCanvas.Children.Remove(title);
            //MainCanvas.Children.Remove(tutorial);
            MainCanvas.Children.Add(menu);
        }

        public void GameStart()
        {
            newgame = new GameWindow();

            MainCanvas.Children.Remove(menu);
            MainCanvas.Children.Add(newgame);
            title.MouseDown -= Title_MouseDown;
        }

        public void GameEnd()
        {
            endwindow = new EndGame();

            newAudioManager.StopTrack();
            MainCanvas.Children.Remove(newgame);
            MainCanvas.Children.Add(endwindow);
        }

        public static void GameRestart()
        {
            //MessageBox.Show("Restarting.");

            System.Windows.Forms.Application.Exit();
            System.Windows.Forms.Application.Restart();
        }

        private void volume_off_On()
        {
            // Event run when the audio button is pressed (either click or touch) - essentially turns audio on or off, and swaps the image to match. 
            // (On turning the audio on) Load a new instance of Audio Manager, and play the main music track
            // (On turning the audio off) unload the current instance of audio manager, and stock the music track
            if (!GlobalAudio1)
            {
                GlobalAudio1 = true;
                volImage.Source = new BitmapImage(new Uri("Resources/audioOn.png", UriKind.Relative));
                newAudioManager = new AudioManager();
                newAudioManager.playMainMusic();
            }
            else
            {
                GlobalAudio1 = false;
                volImage.Source = new BitmapImage(new Uri("Resources/audio.png", UriKind.Relative));
                newAudioManager.StopTrack();
                newAudioManager = null;
            }
            // UPDATE AUDIO MANAGER AS TO WHETHER AUDIO SHOULD BE ON
        }

        public void ToggleVolumeButton()
        {
            //MessageBox.Show("Volume button toggled.");
            if (volLabel.IsEnabled && volImage.Source != null)
            {
                volLabel.IsEnabled = false;
                volImage.Source = null;
            }

            else
            {
                volLabel.IsEnabled = true;
                volImage.Source = new BitmapImage(new Uri("Resources/audioOn.png", UriKind.Relative));
            }
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            volume_off_On();
        }
        private void image_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            volume_off_On();
        }

        //
        public static MainWindow ReturnMainWindowInstance()
        {
            return mainWin;
        }
    }
}
