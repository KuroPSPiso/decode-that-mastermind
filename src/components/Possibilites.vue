<template>
  <div>
    <!--temp active codes list-->
    <v-list>
      <v-list-item v-for="code in temp_codeArr" :key="code.numbers">
          <v-list-item-action>
              <v-icon icon="mdi-delete"></v-icon>
          </v-list-item-action>
          <v-list-item-content>
              <v-list-item-title>{{code.numbers}} {{code.numCorrect}}</v-list-item-title>
          </v-list-item-content>
      </v-list-item>
    </v-list>

    <!--code possibilities-->
    <!-- <div v-if="!isPocessing" v-html="possibilitiesRender" /> -->
    <div class="possibilities" v-if="!isPocessing" v-html="possibilitiesRender" />
      <!-- {{possibilitiesView}} 
    </div> -->
    <div v-else>
      processing possibilities...
    </div>
    <!-- <v-list>
      <v-list-item v-for="code in possibilities" :key="code">
          <v-list-item-content>
              <v-list-item-title>{{code.x1}}{{code.x2}}{{code.x3}}{{code.x4}}{{code.x5}}</v-list-item-title>
          </v-list-item-content>
      </v-list-item>
    </v-list> -->
    <!-- <div>
      {{json_map}}
    </div> -->
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
            possibilitiesView: [],
            possibilitiesRender: [],
            isPocessing: false,
            json_map: ''
        }
    },
    methods: {
      async generatePossibilitiesBlank() {
        //load initial Possibilities (static 00000 -> 99999)
        this.possibilities = defaultPossibilities

        this.possibilities.forEach(possibility => {
          this.possibilitiesView.push(`${possibility.x1}${possibility.x2}${possibility.x3}${possibility.x4}${possibility.x5}`)
        })

        this.isPocessing = false;

        /*
        //generate json
        for(var i = 0; i <= 99999; i++){
          const sCode = i.toString().padStart(5, '0')
          const cCharCode = { x1: sCode[0],  x2: sCode[1],  x3: sCode[2],  x4: sCode[3],  x5: sCode[4] } 
          this.possibilities.push(cCharCode)
        }
        this.json_map = JSON.stringify(this.possibilities)*/
      },
      calcPossibility(newCode, numCorrect) {
        if(numCorrect === 0)
        {
          console.log("remove unnecessary")

          //all characters of array can be removed
          const tempPossibilities = []
          this.possibilitiesView = []
          this.possibilities.forEach(possibility => {
            if(
              possibility.x1 !== newCode[0] &&
              possibility.x2 !== newCode[1] &&
              possibility.x3 !== newCode[2] &&
              possibility.x4 !== newCode[3] &&
              possibility.x5 !== newCode[4]
            ) {
              tempPossibilities.push(possibility)
              this.possibilitiesView.push(`${possibility.x1}${possibility.x2}${possibility.x3}${possibility.x4}${possibility.x5}`)
            }
          })
          
          this.possibilities = tempPossibilities
          this.isPocessing = false;
        }
      }
    },
    watch: {
      possibilitiesView(newPossibilitiesView, oldPossibilitiesView)
      {
        var renderStr = newPossibilitiesView.toString()
        renderStr = renderStr.replaceAll(',', `</span><span class="pill">`)
        renderStr = `<span class="pill">${renderStr}</span>`
        this.possibilitiesRender = renderStr
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

        this.isPocessing = true;
        await this.generatePossibilitiesBlank()

        this.isPocessing = true;

        //create temp map with current scores
        this.temp_code_map = new Map()
        for(var i = 0; i < this.temp_codeRange; i++)
        {
          this.calcPossibility(exampleCodes[i].numbers, exampleCodes[i].numCorrect)
          this.temp_code_map.set(
            exampleCodes[i].numbers, 
            {
              numCorrect: exampleCodes[i].numCorrect, 
              x1: exampleCodes[i].numbers[0], 
              x2: exampleCodes[i].numbers[1], 
              x3: exampleCodes[i].numbers[2], 
              x4: exampleCodes[i].numbers[3],
              x5: exampleCodes[i].numbers[4]
            })
        }

        //interactable list (v-for fix for map support)
        this.temp_code_map.forEach((value, key) => {
          this.temp_codeArr.push({ numbers: key, numCorrect: value.numCorrect})
        })

    }
}
</script>

<style>
.v-list, .possibilities{
  height:40vh;
  overflow-y:auto;
}

.pill{
  display: flex;
  padding: 0.5rem;
  margin: 1rem;
  float: left;
  background: #2c7ec5;
  color: #fff;
  border-radius: 1rem;
  font-weight: 400;
}
</style>