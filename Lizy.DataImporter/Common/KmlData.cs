using System;
using System.Xml.Serialization;
using System.Collections.Generic;

///* 
//Licensed under the Apache License, Version 2.0
//http://www.apache.org/licenses/LICENSE-2.0
//Generated with https://xmltocsharp.azurewebsites.net/
//*/

//namespace Lizy.DataImporter.Common
//{
//	[XmlRoot(ElementName = "SimpleField", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class SimpleField
//	{
//		[XmlAttribute(AttributeName = "name")]
//		public string Name { get; set; }
//		[XmlAttribute(AttributeName = "type")]
//		public string Type { get; set; }
//	}

//	[XmlRoot(ElementName = "Schema", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class Schema
//	{
//		[XmlElement(ElementName = "SimpleField", Namespace = "http://www.opengis.net/kml/2.2")]
//		public SimpleField SimpleField { get; set; }
//		[XmlAttribute(AttributeName = "name")]
//		public string Name { get; set; }
//		[XmlAttribute(AttributeName = "id")]
//		public string Id { get; set; }
//	}

//	[XmlRoot(ElementName = "LineStyle", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class LineStyle
//	{
//		[XmlElement(ElementName = "color", Namespace = "http://www.opengis.net/kml/2.2")]
//		public string Color { get; set; }
//	}

//	[XmlRoot(ElementName = "PolyStyle", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class PolyStyle
//	{
//		[XmlElement(ElementName = "fill", Namespace = "http://www.opengis.net/kml/2.2")]
//		public string Fill { get; set; }
//	}

//	[XmlRoot(ElementName = "Style", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class Style
//	{
//		[XmlElement(ElementName = "LineStyle", Namespace = "http://www.opengis.net/kml/2.2")]
//		public LineStyle LineStyle { get; set; }
//		[XmlElement(ElementName = "PolyStyle", Namespace = "http://www.opengis.net/kml/2.2")]
//		public PolyStyle PolyStyle { get; set; }
//	}

//	[XmlRoot(ElementName = "LinearRing", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class LinearRing
//	{
//		[XmlElement(ElementName = "coordinates", Namespace = "http://www.opengis.net/kml/2.2")]
//		public string Coordinates { get; set; }
//	}

//	[XmlRoot(ElementName = "outerBoundaryIs", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class OuterBoundaryIs
//	{
//		[XmlElement(ElementName = "LinearRing", Namespace = "http://www.opengis.net/kml/2.2")]
//		public LinearRing LinearRing { get; set; }
//	}

//	[XmlRoot(ElementName = "Polygon", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class Polygon
//	{
//		[XmlElement(ElementName = "outerBoundaryIs", Namespace = "http://www.opengis.net/kml/2.2")]
//		public OuterBoundaryIs OuterBoundaryIs { get; set; }
//	}

//	[XmlRoot(ElementName = "MultiGeometry", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class MultiGeometry
//	{
//		[XmlElement(ElementName = "Polygon", Namespace = "http://www.opengis.net/kml/2.2")]
//		public Polygon Polygon { get; set; }
//	}

//	[XmlRoot(ElementName = "Placemark", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class Placemark
//	{
//		[XmlElement(ElementName = "name", Namespace = "http://www.opengis.net/kml/2.2")]
//		public string Name { get; set; }
//		[XmlElement(ElementName = "Style", Namespace = "http://www.opengis.net/kml/2.2")]
//		public Style Style { get; set; }
//		[XmlElement(ElementName = "MultiGeometry", Namespace = "http://www.opengis.net/kml/2.2")]
//		public MultiGeometry MultiGeometry { get; set; }
//	}

//	[XmlRoot(ElementName = "Folder", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class Folder
//	{
//		[XmlElement(ElementName = "name", Namespace = "http://www.opengis.net/kml/2.2")]
//		public string Name { get; set; }
//		[XmlElement(ElementName = "Placemark", Namespace = "http://www.opengis.net/kml/2.2")]
//		public List<Placemark> Placemark { get; set; }
//	}

//	[XmlRoot(ElementName = "Document", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class Document
//	{
//		[XmlElement(ElementName = "Schema", Namespace = "http://www.opengis.net/kml/2.2")]
//		public Schema Schema { get; set; }
//		[XmlElement(ElementName = "Folder", Namespace = "http://www.opengis.net/kml/2.2")]
//		public Folder Folder { get; set; }
//		[XmlAttribute(AttributeName = "id")]
//		public string Id { get; set; }
//	}

//	[XmlRoot(ElementName = "kml", Namespace = "http://www.opengis.net/kml/2.2")]
//	public class KmlData
//	{
//		[XmlElement(ElementName = "Document", Namespace = "http://www.opengis.net/kml/2.2")]
//		public Document Document { get; set; }
//		[XmlAttribute(AttributeName = "xmlns")]
//		public string Xmlns { get; set; }
//	}

//}
