<template>
  <div>
    <!--temp active codes list-->
    <v-list>
      <v-list-item v-for="code in temp_codeArr" :key="code.numbers">
          <v-list-item-action>
              <v-icon icon="mdi-delete-outline"></v-icon>
          </v-list-item-action>
          <v-list-item-content>
              <v-list-item-title>{{code.numbers}} {{code.numCorrect}}</v-list-item-title>
          </v-list-item-content>
      </v-list-item>
    </v-list>

    <!--code possibilities-->
    <!-- <v-list>
      <v-list-item v-for="code in possibilities" :key="code">
          <v-list-item-content>
              <v-list-item-title>{{code.x1}}{{code.x2}}{{code.x3}}{{code.x4}}{{code.x5}}</v-list-item-title>
          </v-list-item-content>
      </v-list-item>
    </v-list> -->

    <div>
      {{json_map}}
    </div>
  </div>
</template>

<script>
import defaultPossibilities from '../assets/default_possibilities.json'


export default {
    name: 'Possibilities',
    data() {
        return{
            temp_codeArr: [],
            temp_codeMap: new Map(),
            temp_codeRange: 9,
            possibilities: [],
            json_map: ''
        }
    },
    methods: {
      async generatePossibilitiesBlank() {
        //load initial Possibilities (static 00000 -> 99999)
        this.possibilities = defaultPossibilities

        /*
        for(var i = 0; i <= 99999; i++){
          const sCode = i.toString().padStart(5, '0')
          const cCharCode = { x1: sCode[0],  x2: sCode[1],  x3: sCode[2],  x4: sCode[3],  x5: sCode[4] } 
          this.possibilities.push(cCharCode)
        }
        this.json_map = JSON.stringify(this.possibilities)*/
      }
    },
    computed: {
    },
    async mounted() {
        const correctCode = '51344'
        const exampleCodes = [
            { numbers: '00000', numCorrect: 0 },
            { numbers: '11111', numCorrect: 1 },
            { numbers: '11100', numCorrect: 1 },
            { numbers: '00100', numCorrect: 0 },
            { numbers: '01000', numCorrect: 1 },
            { numbers: '44444', numCorrect: 2 },
            { numbers: '71360', numCorrect: 1 },
            { numbers: '98475', numCorrect: 0 },
            { numbers: '55500', numCorrect: 1 },
            { numbers: '50000', numCorrect: 1 },
        ]

        //create temp map with current scores
        this.temp_code_map = new Map()
        for(var i = 0; i < this.temp_codeRange; i++)
        {
          this.temp_code_map.set(exampleCodes[i].numbers, exampleCodes[i].numCorrect)
        }

        //interactable list (v-for fix for map support)
        this.temp_code_map.forEach((value, key) => {
          this.temp_codeArr.push({ numbers: key, numCorrect: value})
        })

        await this.generatePossibilitiesBlank()
    }
}
</script>

<style>
.v-list{
  height:10rem;
  overflow-y:auto;
}
</style>