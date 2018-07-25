using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Com.Mapbox.Api.Directions.V5;
using Com.Mapbox.Api.Directions.V5.Models;
using Com.Mapbox.Api.Geocoding.V5;
using Com.Mapbox.Api.Geocoding.V5.Models;
using Com.Mapbox.Api.Matching.V5;
using Com.Mapbox.Api.Matching.V5.Models;
using Com.Mapbox.Api.Optimization.V1;
using Com.Mapbox.Api.Optimization.V1.Models;
using Com.Mapbox.Api.Speech.V1;
using Com.Mapbox.Api.Staticmap.V1;
using Com.Mapbox.Api.Staticmap.V1.Models;
using Com.Mapbox.Core.Utils;
using Com.Mapbox.Geojson;
using Com.Mapbox.Geojson.Additions;
using Java.Lang;
using Square.Retrofit2;

namespace Samples
{
    [Activity(Label = "Samples", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var policy = new StrictMode.ThreadPolicy.Builder().PermitAll().Build();
            StrictMode.SetThreadPolicy(policy);

            SetContentView(Resource.Layout.MainLayout);

            var basicDirectionsButton = FindViewById<Button>(Resource.Id.BasicDirectionsButton);
            basicDirectionsButton.Click += (sender, e) =>
            {
                SimpleMapboxDirectionsRequest();
                AsyncMapboxDirectionsRequest();
            };

            var basicFeatureButton = FindViewById<Button>(Resource.Id.BasicFeatureCollectionButton);
            basicFeatureButton.Click += (sender, e) =>
            {
                var featureForJson = new FeatureForJson
                {
                    Geometry = new DatasetGeometry
                    {
                        Coordinates = new List<object> { 1.0, 2.0 },
                        Type = "Point"
                    }
                };

                var feature = Feature.FromJson(featureForJson.ToJson());
                System.Diagnostics.Debug.WriteLine(feature.ToJson());
            };

            var basicGeocodingButton = FindViewById<Button>(Resource.Id.BasicGeocodingButton);
            basicGeocodingButton.Click += (sender, e) =>
            {
                MapboxGeocoding.InvokeBuilder()
                               .AccessToken(GetString(Resource.String.access_token))
                               .Query("1600")
                               .Build()
                               .EnqueueCall(new BasicGeocodingCallback());
            };

            var basicMapMatchingButton = FindViewById<Button>(Resource.Id.BasicMapMatchingButton);
            basicMapMatchingButton.Click += (sender, e) =>
            {
                MapboxMapMatching.InvokeBuilder()
                                 .AccessToken(GetString(Resource.String.access_token))
                                 .Coordinate(Point.FromLngLat(-117.1728265285492, 32.71204416018209))
                                 .Coordinate(Point.FromLngLat(-117.17288821935652, 32.712258556224))
                                 .Coordinate(Point.FromLngLat(-117.17293113470076, 32.712443613445814))
                                 .Coordinate(Point.FromLngLat(-117.17292040586472, 32.71256999376694))
                                 .Coordinate(Point.FromLngLat(-117.17298477888109, 32.712603845608285))
                                 .Coordinate(Point.FromLngLat(-117.17314302921294, 32.71259933203019))
                                 .Coordinate(Point.FromLngLat(-117.17334151268004, 32.71254065549407))
                                 .Build()
                                 .EnqueueCall(new BasicMapMatchingCallback());

            };

            var basicOpmitizationButton = FindViewById<Button>(Resource.Id.BasicOptimizationButton);
            basicOpmitizationButton.Click += (sender, e) =>
            {
                MapboxOptimization.InvokeBuilder()
                                  .AccessToken(GetString(Resource.String.access_token))
                                  .Coordinate(Point.FromLngLat(-122.42, 37.78))
                                  .Coordinate(Point.FromLngLat(-122.45, 37.91))
                                  .Coordinate(Point.FromLngLat(-122.48, 37.73))
                                  .Build()
                                  .EnqueueCall(new BasicOpmitizationCallback());
            };

            var basicSpeechButton = FindViewById<Button>(Resource.Id.BasicSpeechButton);
            basicSpeechButton.Click += (sender, e) =>
            {
                MapboxSpeech.InvokeBuilder()
                            .AccessToken(GetString(Resource.String.access_token))
                            .Instruction("turn right")
                            .Build()
                            .EnqueueCall(new BasicSpeechCallback());
            };

            var basicStatisMapButton = FindViewById<Button>(Resource.Id.BasicStaticMapButton);
            basicStatisMapButton.Click += (sender, e) =>
            {
                List<StaticMarkerAnnotation> markers = new List<StaticMarkerAnnotation>();
                List<StaticPolylineAnnotation> polylines = new List<StaticPolylineAnnotation>();

                markers.Add(StaticMarkerAnnotation.InvokeBuilder()
                            .Name(StaticMapCriteria.LargePin)
                            .Lnglat(Point.FromLngLat(-122.46589, 37.77343))
                            .Color(ColorUtils.ToHexString(
                                Android.Graphics.Color.Magenta.R,
                                Android.Graphics.Color.Magenta.G,
                                Android.Graphics.Color.Magenta.B))
                            .Label("a")
                            .Build());

                markers.Add(StaticMarkerAnnotation.InvokeBuilder()
                            .Name(StaticMapCriteria.LargePin)
                            .Lnglat(Point.FromLngLat(-122.42816, 37.75965))
                            .Color(ColorUtils.ToHexString(
                                Android.Graphics.Color.Magenta.R,
                                Android.Graphics.Color.Magenta.G,
                                Android.Graphics.Color.Magenta.B))
                            .Label("b")
                            .Build());

                polylines.Add(StaticPolylineAnnotation.InvokeBuilder().Polyline("abcdef").Build());

                var mapboxStaticMap = MapboxStaticMap.InvokeBuilder()
                                                     .AccessToken(GetString(Resource.String.access_token))
                                                     .Width(500)
                                                     .Height(300)
                                                     .Retina(true)
                                                     .CameraAuto(true)
                                                     .StaticMarkerAnnotations(markers)
                                                     .StaticPolylineAnnotations(polylines)
                                                     .Build();
                System.Diagnostics.Debug.WriteLine(mapboxStaticMap.Url());
            };
        }

        // Demonstrates how to make the most basic directions request.
        void SimpleMapboxDirectionsRequest()
        {
            var builder = MapboxDirections.InvokeBuilder();

            // 1. Pass in all the required information to get a simple directions route.
            builder.AccessToken(GetString(Resource.String.access_token));
            builder.Origin(Point.FromLngLat(-95.6332, 29.7890));
            builder.Destination(Point.FromLngLat(-95.3591, 29.7576));

            // 2. That's it! Now execute the command and get the response.
            var response = builder.Build().ExecuteCall();

            // 3. Log information from the response
            System.Diagnostics.Debug.WriteLine("From SimpleMapboxDirectionsRequest");
            System.Diagnostics.Debug.WriteLine(
                $"Check that the response is successful {response.IsSuccessful}");

            var body = response.Body() as DirectionsResponse;

            System.Diagnostics.Debug.WriteLine(
                $"Get the first routes distance from origin to destination: {body.Routes()[0].Distance()}");
        }

        // Demonstrates how to make an asynchronous directions request.
        void AsyncMapboxDirectionsRequest()
        {
            // 1. Pass in all the required information to get a route.
            var request = MapboxDirections.InvokeBuilder()
                                          .AccessToken(GetString(Resource.String.access_token))
                                          .Origin(Point.FromLngLat(-95.6332, 29.7890))
                                          .Destination(Point.FromLngLat(-95.3591, 29.7576))
                                          .Profile(DirectionsCriteria.ProfileCycling)
                                          .Steps((Java.Lang.Boolean)true)
                                          .Build();

            // 2. Now request the route using a async call
            request.EnqueueCall(new AsyncMapboxDirectionsCallback());
        }

        class AsyncMapboxDirectionsCallback : Java.Lang.Object, ICallback
        {
            public void OnFailure(ICall p0, Throwable p1)
            {
                System.Diagnostics.Debug.WriteLine(p1.Message);
            }

            public void OnResponse(ICall p0, Response p1)
            {
                // 3. Log information from the response
                if (p1.IsSuccessful)
                {
                    var body = p1.Body() as DirectionsResponse;

                    System.Diagnostics.Debug.WriteLine("From AsyncMapboxDirectionsRequest");
                    System.Diagnostics.Debug.WriteLine(
                        $"Get the street name of the first step along the route: {body.Routes()[0].Legs()[0].Steps()[0].Name()}");
                }
            }
        }

        class BasicGeocodingCallback : Java.Lang.Object, ICallback
        {
            public void OnFailure(ICall p0, Throwable p1)
            {
                System.Diagnostics.Debug.WriteLine(p1.Message);
            }

            public void OnResponse(ICall p0, Response p1)
            {
                var body = p1.Body() as GeocodingResponse;
                System.Diagnostics.Debug.WriteLine(body.Type());
            }
        }

        class BasicMapMatchingCallback : Java.Lang.Object, ICallback
        {
            public void OnFailure(ICall p0, Throwable p1)
            {
                System.Diagnostics.Debug.WriteLine(p1.Message);
            }

            public void OnResponse(ICall p0, Response p1)
            {
                var body = p1.Body() as MapMatchingResponse;
                System.Diagnostics.Debug.WriteLine(body.ToString());
            }
        }

        class BasicOpmitizationCallback : Java.Lang.Object, ICallback
        {
            public void OnFailure(ICall p0, Throwable p1)
            {
                System.Diagnostics.Debug.WriteLine(p1.Message);
            }

            public void OnResponse(ICall p0, Response p1)
            {
                var body = p1.Body() as OptimizationResponse;
                System.Diagnostics.Debug.WriteLine(body.Code());
            }
        }

        class BasicSpeechCallback : Java.Lang.Object, ICallback
        {
            public void OnFailure(ICall p0, Throwable p1)
            {
                System.Diagnostics.Debug.WriteLine(p1.Message);
            }

            public void OnResponse(ICall p0, Response p1)
            {
                var body = p1.Body() as Square.OkHttp3.ResponseBody;
                System.Diagnostics.Debug.WriteLine(body.ContentType());
            }
        }
    }
}

