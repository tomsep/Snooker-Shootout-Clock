# Snooker Shootout -Pelikello
(English below)

## Asennus 
Vain Android yhteensopiva. 

1. Lataa viimeisin versio [tästä](https://github.com/tomsep/Snooker-Shootout-Clock/releases/download/v1.0.0-beta.1/snooker-shootout-clock.apk). 
2. Avaa ja asenna **snooker-shootout-clock.apk** tiedosto. Koska tämä sovellus ei ainakaan vielä ole saatavilla Google Play -kaupassa **tarvitsee ulkopuolisista lähteistä asentamiseen antaa erillinen lupa**. Katso lisätietoja [sovelluksen lataaminen muista lähteistä](https://support.google.com/pixelphone/answer/7391672?hl=fi#zippy=%2Csovellusten-lataaminen-muista-l%C3%A4hteist%C3%A4).

## Sovelluksen ominaisuudet 

* pelikello 10min (säädettävissä) 
* lyöntikello 15s. Vaihtuu puoliajan jälkeen 10s. Kaukosäätimellä etäohjattavissa. 
* väliaikailmoitukset englanniksi (puoliaika, 1min, 30s, 15s)
* pelin pysäytys


## Lyöntikellon etäohjaus

Lyöntikello voidaan käynnistää ja pysäyttää etäohjauksella. Tarvitset Android yhteensopivan Bluetooth yhteydellä toimivan kameran etälaukaisimen. Tässä esimerkiksi Clash Ohlsonilta sellainen [linkki](https://www.clasohlson.com/fi/Puhelimen-kaukolaukaisin/p/38-8610).

Yhdistä Bluetooth laite puhelimeesi. Sovelluksen ollessa auki etälaukaisimen painike tulkitaan lyöntikellon painamisena. Joka painalluksella puhelimen ääniasetus-valikko saattaa ponnahtaa hetkeksi näkyviin. Tämä johtuu siitä että taustalla etälaukaisin oikeasti ohjaa ääniasetusta, joka sovelluksessa tulkitaan lyöntikellon painamisena.

# Snooker Shootout Clock
Remote controllable Android application for tracking both frame time and shot time during a snooker shootout game.

https://github.com/tomsep/Snooker-Shootout-Clock/assets/52826851/9d0f9b1e-a9e6-455d-bc4a-f7f3e27072b5

## Features
* Frame and shot timers.
* Shot time is automatically reduced from 15s to 10s after half-time is passed.
* Remote controllable shot timer. Compatible with most camera shutter remotes.
* Timer countdown sound effects.
* Half-time, last minute, last 30s, and last 15s announcement sounds.

### Building 
- In a terminal window, navigate to the project's root directory and run the following command:
```
./gradlew assemble
```
- On successful completion of the build, the output files can be found in
  [`plugin/demo/addons`](plugin/demo/addons)

- Next build the Godot 4.2 poject in [`plugin/demo`](plugin/demo)
- Export the project from Godot to Android.
