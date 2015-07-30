using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace Botting.Hero.Classes
{
	static class BitmapExtensions
	{
//		private const Int32 Divider = 4; // Compare to images at x4 time smaller then real resolution for speed
		public static TemplateMatch[] Contains(this Bitmap Template, Bitmap bmp, float Precision = 0.9f, Int32 Dividier = 4)
		{
			var ETM = new ExhaustiveTemplateMatching(Precision);

			try
			{
				var Matches = ETM.ProcessImage(
					new ResizeNearestNeighbor(Template.Width / Dividier, Template.Height / Dividier).Apply(Template),
					new ResizeNearestNeighbor(bmp.Width / Dividier, bmp.Height / Dividier).Apply(bmp)
				);

				if (Matches.Any())
					return Matches;
				else return null;
			}
			catch (Exception Ex)
			{
				Console.WriteLine(Ex);
				return null;
			}
		}
	}
}
