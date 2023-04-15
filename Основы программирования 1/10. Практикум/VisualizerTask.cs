using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Manipulation
{
	public static class VisualizerTask
	{
		public static double X = 220;
		public static double Y = -100;
		public static double Alpha = 0.05;
		public static double Wrist = 2 * Math.PI / 3;
		public static double Elbow = 3 * Math.PI / 4;
		public static double Shoulder = Math.PI / 2;

		public static Brush UnreachableAreaBrush = new SolidBrush(Color.FromArgb(255, 255, 230, 230));
		public static Brush ReachableAreaBrush = new SolidBrush(Color.FromArgb(255, 230, 255, 230));
		public static Pen ManipulatorPen = new Pen(Color.Black, 3);
		public static Brush JointBrush = Brushes.Gray;

		public static void KeyDown(Form form, KeyEventArgs key)
		{
			if (key.KeyCode == Keys.Q) Shoulder += Alpha;
            if (key.KeyCode == Keys.A) Shoulder -= Alpha;

            if (key.KeyCode == Keys.S) Elbow -= Alpha;
            if (key.KeyCode == Keys.W) Elbow += Alpha;

            Wrist = -Alpha - Shoulder - Elbow;
            form.Invalidate();
		}


		public static void MouseMove(Form form, MouseEventArgs mousePosition)
		{
			var position = ConvertWindowToMath(new PointF ( mousePosition.X, mousePosition.Y ), GetShoulderPos(form));
			X = position.X;
			Y = position.Y;
			UpdateManipulator();
			form.Invalidate();
		}

		public static void MouseWheel(Form form, MouseEventArgs e)
		{
			var wheelMove = e.Delta;
			Alpha += wheelMove;

			UpdateManipulator();
			form.Invalidate();
		}

		public static void UpdateManipulator()
		{
			var angles = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
			if (angles[1] == double.NaN) return;
			Shoulder = angles[0];
			Elbow = angles[1];
			Wrist = angles[2];
		}

		public static void DrawManipulator(Graphics graphics, PointF shoulderPosition)
		{
			var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

			graphics.DrawString(
                $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}", 
                new Font(SystemFonts.DefaultFont.FontFamily, 12), 
                Brushes.DarkRed, 
                10, 
                10);
			DrawReachableZone(graphics, ReachableAreaBrush, UnreachableAreaBrush, shoulderPosition, joints);

            for (int i = 0; i < joints.Length; i++)
                ConvertMathToWindow(joints[i], shoulderPosition);
			DrawPartsOfManipulator(joints, shoulderPosition, graphics);
			DrawJointsOfManipulator(joints, graphics);
        }
        public static void DrawPartsOfManipulator(PointF[] joints, PointF shoulderPosition, Graphics graphics)
		{
			var movingElementsOfManipulator = 2;
            for (int i = 0; i < movingElementsOfManipulator; i++)
                graphics.DrawLine(ManipulatorPen, joints[i], joints[i + 1]);

            graphics.DrawLine(ManipulatorPen, shoulderPosition, joints[0]);
        }
		public static void DrawJointsOfManipulator(PointF[] joints, Graphics graphics)
		{
            for (int i = 0; i < joints.Length; i++)
                graphics.FillEllipse(JointBrush, joints[i].X, joints[i].Y, 1, 1);
        }

        private static void DrawReachableZone(
            Graphics graphics, 
            Brush reachableBrush, 
            Brush unreachableBrush, 
            PointF shoulderPos, 
            PointF[] joints)
		{
			var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
			var rmax = Manipulator.UpperArm + Manipulator.Forearm;
			var mathCenter = new PointF(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
			var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
			graphics.FillEllipse(reachableBrush, windowCenter.X - rmax, windowCenter.Y - rmax, 2 * rmax, 2 * rmax);
			graphics.FillEllipse(unreachableBrush, windowCenter.X - rmin, windowCenter.Y - rmin, 2 * rmin, 2 * rmin);
		}

		public static PointF GetShoulderPos(Form form)
		{
			return new PointF(form.ClientSize.Width / 2f, form.ClientSize.Height / 2f);
		}

		public static PointF ConvertMathToWindow(PointF mathPoint, PointF shoulderPos)
		{
			return new PointF(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
		}

		public static PointF ConvertWindowToMath(PointF windowPoint, PointF shoulderPos)
		{
			return new PointF(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
		}
	}
}