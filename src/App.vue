<template>
  <v-app>
    <v-app-bar
      app
      color="primary"
      dark
    >
      <div class="d-flex align-center">
        <span class="shrink mt-1 hidden-sm-and-down">538's "CODE = CASHEN" Cracker</span>
      </div>

      <v-spacer></v-spacer>

      <v-btn
        href="https://github.com/KuroPSPiso/decode-that-mastermind"
        target="_blank"
        text
      >
        <span class="mr-2">Latest Release</span>
        <v-icon>mdi-open-in-new</v-icon>
      </v-btn>
    </v-app-bar>

    <v-main>
      <Possibilities/>
      <!-- <HelloWorld/> -->
    </v-main>
  </v-app>
</template>

<script>
import Possibilities from './components/Possibilites.vue';

export default {
  name: 'App',

  components: {
    Possibilities
  },

  data: () => ({
    //
  }),
};
</script>
