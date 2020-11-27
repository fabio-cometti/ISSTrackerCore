using System.Windows.Forms;

namespace ISSTracker
{
    partial class MapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private GMap.NET.WindowsForms.GMapControl MainMap = null;
        private Label SpeedLabel = null;
        private Label LatitudeLabel = null;
        private Label LongitudeLabel = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
            this.RefreshMapTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // RefreshMapTimer
            // 
            this.RefreshMapTimer.Interval = 1000;
            this.RefreshMapTimer.Tick += new System.EventHandler(this.RefreshMapTimer_Tick);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapForm";
            this.Text = "Map";
            this.Load += MapForm_Load;

            //this.SpeedLabel = new Label();
            //this.SpeedLabel.Location = new System.Drawing.Point(10, 10);
            //this.SpeedLabel.Size = new System.Drawing.Size(200, 16);            
            //this.SpeedLabel.BackColor = System.Drawing.Color.SteelBlue;  
            //this.Controls.Add(this.SpeedLabel);

            this.LatitudeLabel = new Label();
            this.LatitudeLabel.Location = new System.Drawing.Point(412, 10);
            this.LatitudeLabel.Size = new System.Drawing.Size(200, 16);
            this.LatitudeLabel.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.LatitudeLabel);

            this.LongitudeLabel = new Label();
            this.LongitudeLabel.Location = new System.Drawing.Point(814, 10);
            this.LongitudeLabel.Size = new System.Drawing.Size(200, 16);
            this.LongitudeLabel.BackColor = System.Drawing.Color.SteelBlue;
            this.Controls.Add(this.LongitudeLabel);

            this.MainMap = new GMap.NET.WindowsForms.GMapControl();
            this.MainMap.Bearing = 0F;
            this.MainMap.CanDragMap = true;
            this.MainMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.MainMap.GrayScaleMode = false;
            this.MainMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.MainMap.LevelsKeepInMemory = 5;
            this.MainMap.Location = new System.Drawing.Point(0, 0);
            this.MainMap.MarkersEnabled = true;
            this.MainMap.MaxZoom = 2;
            this.MainMap.MinZoom = 2;
            this.MainMap.MouseWheelZoomEnabled = true;
            this.MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.MainMap.Name = "MainMap";
            this.MainMap.NegativeMode = false;
            this.MainMap.PolygonsEnabled = true;
            this.MainMap.RetryLoadTile = 0;
            this.MainMap.RoutesEnabled = true;
            this.MainMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.MainMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.MainMap.ShowTileGridLines = false;
            this.MainMap.TabIndex = 4;
            this.MainMap.Zoom = 0D;
            this.MainMap.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.MainMap);

            this.ResumeLayout(false);
        }



        #endregion

        private System.Windows.Forms.Timer RefreshMapTimer;
    }
}