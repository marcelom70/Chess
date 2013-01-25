using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace WebChess
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WCFChess.iChessClient newChessClient;
            newChessClient = new WCFChess.iChessClient();
            bool init = newChessClient.InitializeBoard();
            if (!init){
                 //Chess can't be instantied
            } else {
                Response.ContentType = "image/jpeg";
                int sqX=1;
                int sqY=1;
                Bitmap sqBitmap = new Bitmap(489, 489);
                Graphics formGraphics = Graphics.FromImage(sqBitmap);
                Color sqColor;
                Pen sqPen;
                int increaseFactor = 60;

                for (int rows = 0; rows <= 7; rows++)
                {
                    for (int squares=0;squares<=7;squares++){
                        if (squares % 2 == 0)
                        {
                            sqColor = Color.WhiteSmoke;
                            SolidBrush sqBrush = new System.Drawing.SolidBrush(sqColor);
                            formGraphics.FillRectangle(sqBrush, new Rectangle(sqX, sqY, (increaseFactor + 1), increaseFactor));
                        }
                        else
                        {
                            if (sqY > 1)
                            {
                                sqPen = new Pen(Color.WhiteSmoke);
                                formGraphics.DrawLine(sqPen, new Point(sqX, sqY), new Point((sqX + (increaseFactor + 1)), sqY));
                            }
                        }
                        sqX += (increaseFactor + 1);
                    }
                    sqY += (increaseFactor + 1);
                    sqX=1;
                }
                sqBitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
