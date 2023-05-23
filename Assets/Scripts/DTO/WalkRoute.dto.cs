using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System;

namespace DataTypes
{
	public partial class WalkRoute
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("features")]
		public Feature[] Features { get; set; }
	}

	public partial class Feature
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("geometry")]
		public Geometry Geometry { get; set; }

		[JsonProperty("properties")]
		public Properties Properties { get; set; }
	}

	public partial class Geometry
	{
		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("coordinates")]
		public Coordinate[] Coordinates { get; set; }
	}

	public partial class Properties
	{
		[JsonProperty("totalDistance", NullValueHandling = NullValueHandling.Ignore)]
		public long? TotalDistance { get; set; }

		[JsonProperty("totalTime", NullValueHandling = NullValueHandling.Ignore)]
		public long? TotalTime { get; set; }

		[JsonProperty("index")]
		public long Index { get; set; }

		[JsonProperty("pointIndex", NullValueHandling = NullValueHandling.Ignore)]
		public long? PointIndex { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
		public string Direction { get; set; }

		[JsonProperty("nearPoiName", NullValueHandling = NullValueHandling.Ignore)]
		public string NearPoiName { get; set; }

		[JsonProperty("nearPoiX", NullValueHandling = NullValueHandling.Ignore)]
		public string NearPoiX { get; set; }

		[JsonProperty("nearPoiY", NullValueHandling = NullValueHandling.Ignore)]
		public string NearPoiY { get; set; }

		[JsonProperty("intersectionName", NullValueHandling = NullValueHandling.Ignore)]
		public string IntersectionName { get; set; }

		[JsonProperty("facilityType")]
		public string FacilityType { get; set; }

		[JsonProperty("facilityName")]
		public string FacilityName { get; set; }

		[JsonProperty("turnType", NullValueHandling = NullValueHandling.Ignore)]
		public long? TurnType { get; set; }

		[JsonProperty("pointType", NullValueHandling = NullValueHandling.Ignore)]
		public string PointType { get; set; }

		[JsonProperty("lineIndex", NullValueHandling = NullValueHandling.Ignore)]
		public long? LineIndex { get; set; }

		[JsonProperty("distance", NullValueHandling = NullValueHandling.Ignore)]
		public long? Distance { get; set; }

		[JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
		public long? Time { get; set; }

		[JsonProperty("roadType", NullValueHandling = NullValueHandling.Ignore)]
		public long? RoadType { get; set; }

		[JsonProperty("categoryRoadType", NullValueHandling = NullValueHandling.Ignore)]
		public long? CategoryRoadType { get; set; }
	}

	public partial struct Coordinate
	{
		public double? Double;
		public double[] DoubleArray;

		public static implicit operator Coordinate(double Double) => new Coordinate { Double = Double };
		public static implicit operator Coordinate(double[] DoubleArray) => new Coordinate { DoubleArray = DoubleArray };
	}

	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				CoordinateConverter.Singleton,
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}

	internal class CoordinateConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(Coordinate) || t == typeof(Coordinate?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Integer:
				case JsonToken.Float:
					var doubleValue = serializer.Deserialize<double>(reader);
					return new Coordinate { Double = doubleValue };
				case JsonToken.StartArray:
					var arrayValue = serializer.Deserialize<double[]>(reader);
					return new Coordinate { DoubleArray = arrayValue };
			}
			throw new Exception("Cannot unmarshal type Coordinate");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			var value = (Coordinate)untypedValue;
			if (value.Double != null)
			{
				serializer.Serialize(writer, value.Double.Value);
				return;
			}
			if (value.DoubleArray != null)
			{
				serializer.Serialize(writer, value.DoubleArray);
				return;
			}
			throw new Exception("Cannot marshal type Coordinate");
		}

		public static readonly CoordinateConverter Singleton = new CoordinateConverter();
	}


}