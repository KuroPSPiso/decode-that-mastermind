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
    <v-list>
      <v-list-item v-for="code in possibilities" :key="code">
          <v-list-item-content>
              <v-list-item-title>{{code}}</v-list-item-title>
          </v-list-item-content>
      </v-list-item>
    </v-list>
  </div>
</template>

<script>
export default {
    name: 'Possibilities',
    data() {
        return{
            temp_codeArr: [],
            temp_codeMap: new Map(),
            temp_codeRange: 9,
            possibilities: []
        }
    },
    methods: {
      async generatePossibilitiesBlank() {
        //generate initialPossibilities
        for(var i = 0; i <= 9; i++){
          const sCode = i.toString().padStart(5, '0')
          this.possibilities.push(sCode)
        }
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

        this.generatePossibilitiesBlank()
    }
}
</script>

<style>
.v-list{
  height:10rem;
  overflow-y:auto;
}
</style>