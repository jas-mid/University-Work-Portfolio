# Labs 02–03 — Fuel Cost Sharing App

[← Back to module](../../README.md)

A cross-platform Flutter app that splits the cost of a fuel bill between everyone in the car, with an optional tip for the driver.

## Features

- Enter the total fuel cost in pounds
- Set how many travellers are sharing the cost (never fewer than one)
- Add a tip of 0–50% with a slider
- See each traveller's share update as you type

## Built with

- **Flutter** and **Dart**, using Material 3 with a seeded colour scheme
- **Stateful widgets**: all inputs feed a single derived cost-per-traveller value
- **Widget tests** covering the default state, splitting, the minimum split and the tip

## Development

The app was built in stages across Labs 02 and 03, from the first project setup to the finished app.

## Running the app

```bash
cd fuel_cost_share_app
flutter pub get
flutter run
```

## Running the tests

```bash
cd fuel_cost_share_app
flutter test
```
