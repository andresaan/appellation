# Appellation Song Recommendations

Get fresh new music using Appelation, a song recommendation app that uses your preferences to curate a unique song list just for you.

## Features:

- Song Recommendations: Generate song recommendations based on your favorite artists, genres, and tracks.
  
- Previews: Listen to a sample of recommended tracks to see which ones you like.

- Favorites: Add songs you like to your favorites!

- Spotify API: Our app is powered by the Spotify API, ensuring a wide range of music is available.

- User-Friendly Interface: Intuitive design makes it easy for users to navigate the song generator seamlessly.

## Tour of the App:

### Log In Screen
Here users log in using their Spotify credentials.  

![Log in screen](/FunctionalityImages/LogInScreen.png)

### Home Screen
After logging in, users are redirected to the home page.  

![Home screen](FunctionalityImages/Home.png)

### Song Generator
Here users can input artists, genres, and tracks they want to base their song recommendations on. Users also have the option to choose whether the recommendations provided are "popular" or not,
which is determined based on the streams the track receives on Spotify. Users can disable this feature if they would prefer to receive recommendations with a mix of different popularities.   

![Song generator](FunctionalityImages/SongGenerator.png)

This image shows the song generator with input.   

![Song generator with input](FunctionalityImages/GeneratorWithInput.png)

### Verification Page
Here users will verify their input on the previous page. With Spotify being home to an immense amount of different artists, some songs and artists have the same names. That being said, to generate accurate
recommendations the user is required to verify their original input.    

![User input verification page](FunctionalityImages/Verify.png)

This image shows what verification looks like after selections are made. Here, the user selected the images corresponding to the correct artist and track. The images not selected are dim while the selections are slightly brighter.    

![User input verification page with selections](FunctionalityImages/SeedVerification.png)

This image shows the bottom of the verification page which shows the genre, limit, and popularity settings.    

![Bottom of verification page](FunctionalityImages/SeedVerificationBottom.png)

### Recommendations
Here the user's recommendations are displayed. Tracks can be previewed using the preview button which will play a sample of the song.    

![Recommendations page with song recommendations in a grid layout](FunctionalityImages/Recommendations.png)

This image shows how users can "like" tracks using the heart at the bottom right corner. When the heart is clicked, the heart is filled red denoting the song is in the user's favorites.     

![Another view of the recommendations page with a few songs "liked" which is denoted by a filled red heart](FunctionalityImages/RecommendaitonsFull.png)

### Favorites
The favorites page is where user's liked songs are displayed. Users can like and unlike songs from this page as well as navigate directly to the generator.     

![Favorites page with "liked" songs visible](FunctionalityImages/Favorites.png)

## How to Use: 

### Cloning the repository
```
git clone https://github.com/andresaan/appellation.git
```
### Create Spotify Developer Account
go to https://developer.spotify.com/ and create an account. If you already have a spotify account, you can use your personal credentials.

### Create App Settings 
```
cd appellation/appellation
touch appsettings.json
```

### Configure App Settings
open appsettings.json in a text editor and paste the following. Make sure to input the Client Id and Secret associated with the Spotify Developer account you created.
```
{
  "Spotify": {
    "ClientId": "input your id here",
    "ClientSecret": "input your secret here",
    "RedirectUri": "https://localhost:7040/home"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Run the project
Run project in visual studio and Open http://localhost:4000