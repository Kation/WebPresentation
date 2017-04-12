﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.Web.Media
{
    public class Arc : Shape
    {
        public static readonly DependencyProperty PointProperty = DependencyProperty.Register("Point", typeof(Point), typeof(Arc));
        public Point Point { get { return (Point)GetValue(PointProperty); } set { SetValue(PointProperty, value); } }

        public static readonly DependencyProperty OuterRadiusProperty = DependencyProperty.Register("OuterRadius", typeof(double), typeof(Arc));
        public double OuterRadius { get { return (double)GetValue(OuterRadiusProperty); } set { SetValue(OuterRadiusProperty, value); } }

        public static readonly DependencyProperty InnerRadiusProperty = DependencyProperty.Register("InnerRadius", typeof(double), typeof(Arc));
        public double InnerRadius { get { return (double)GetValue(InnerRadiusProperty); } set { SetValue(InnerRadiusProperty, value); } }

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(double), typeof(Arc));
        public double Angle { get { return (double)GetValue(AngleProperty); } set { SetValue(AngleProperty, value); } }
    }
}
