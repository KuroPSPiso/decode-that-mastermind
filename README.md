# decode-that-mastermind

A (static) site to manage codes and get possible solutions for (NL) Radio 538's 'Code = Cashen' game.

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

## Project details
### Main Packages
* Vue 2
* Vuetify

### Functionality:

* Adding called-in codes to a called-in codes list
  * Entry field for each called-in code consisting out of 5 numbers
  * Entry field for how many numbers in that code are correct (correct positioning is unknown)
* Remove item from called-in codes list
* Device storage
* List of possibly entries with all code positions (per code to the power of 5)
