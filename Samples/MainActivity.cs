using Android.App;
using Android.OS;
using Android.Widget;
using Com.Mapbox.Api.Directions.V5;
using Com.Mapbox.Api.Directions.V5.Models;
using Com.Mapbox.Geojson;
using Java.Lang;
using Square.Retrofit2;

namespace Samples
{
    [Activity(Label = "Samples", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity, ICallback
    {
        #region ICallback
        public void OnFailure(ICall call, Throwable throwable)
        {
            System.Diagnostics.Debug.WriteLine(throwable.Message);
        }

        public void OnResponse(ICall call, Response response)
        {
            // 3. Log information from the response
            if (response.IsSuccessful)
            {
                var body = response.Body() as DirectionsResponse;

                System.Diagnostics.Debug.WriteLine("From AsyncMapboxDirectionsRequest");
                System.Diagnostics.Debug.WriteLine(
                    $"Get the street name of the first step along the route: {body.Routes()[0].Legs()[0].Steps()[0].Name()}");
            }
        }
        #endregion

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
                                          .Steps((Boolean)true)
                                          .Build();

            // 2. Now request the route using a async call
            request.EnqueueCall(this);
        }
    }
}

