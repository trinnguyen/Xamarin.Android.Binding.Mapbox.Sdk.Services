using System;
using Android.Runtime;

namespace Com.Mapbox.Api.Matching.V5
{
    public abstract partial class MapboxMapMatching
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='com.mapbox.api.matching.v5']/class[@name='MapboxMapMatching']/method[@name='baseUrl' and count(parameter)=0]"
        [Register("baseUrl", "()Ljava/lang/String;", "GetBaseUrlHandler")]
        protected override abstract string BaseUrl();
    }
}

namespace Com.Mapbox.Api.Matrix.V1
{
    public abstract partial class MapboxMatrix
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='com.mapbox.api.matrix.v1']/class[@name='MapboxMatrix']/method[@name='baseUrl' and count(parameter)=0]"
        [Register("baseUrl", "()Ljava/lang/String;", "GetBaseUrlHandler")]
        protected override abstract string BaseUrl();
    }
}

namespace Com.Mapbox.Api.Geocoding.V5
{
    public abstract partial class MapboxGeocoding
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='com.mapbox.api.geocoding.v5']/class[@name='MapboxGeocoding']/method[@name='baseUrl' and count(parameter)=0]"
        [Register("baseUrl", "()Ljava/lang/String;", "GetBaseUrlHandler")]
        protected override abstract string BaseUrl();
    }
}

namespace Com.Mapbox.Api.Optimization.V1
{
    public abstract partial class MapboxOptimization
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='com.mapbox.api.optimization.v1']/class[@name='MapboxOptimization']/method[@name='baseUrl' and count(parameter)=0]"
        [Register("baseUrl", "()Ljava/lang/String;", "GetBaseUrlHandler")]
        protected override abstract string BaseUrl();
    }
}

namespace Com.Mapbox.Api.Speech.V1
{
    public abstract partial class MapboxSpeech
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='com.mapbox.api.speech.v1']/class[@name='MapboxSpeech']/method[@name='baseUrl' and count(parameter)=0]"
        [Register("baseUrl", "()Ljava/lang/String;", "GetBaseUrlHandler")]
        protected override abstract string BaseUrl();
    }
}

namespace Com.Mapbox.Api.Directions.V5
{
    public abstract partial class MapboxDirections
    {
        // Metadata.xml XPath method reference: path="/api/package[@name='com.mapbox.api.directions.v5']/class[@name='MapboxDirections']/method[@name='baseUrl' and count(parameter)=0]"
        [Register("baseUrl", "()Ljava/lang/String;", "GetBaseUrlHandler")]
        protected override abstract string BaseUrl();
    }
}
