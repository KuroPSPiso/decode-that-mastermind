<template>
  <div>
    <div class="test-entries" v-if="debug">
      <p v-for="index in temp_entries" :key="index.numbers">{{index.numbers}} ({{index.numCorrect}})</p>
    </div>

    <!--code possibilities-->
    <code class="possibilities" v-if="!isProcessing" v-html="possibilitiesRender" />
    <div v-else>
      processing possibilities...
    </div>
  </div>
</template>

<script>
import defaultPossibilities from '../assets/default_possibilities.json'

export default {
    name: 'Possibilities',
    data() {
        return{
            debug: false,
            temp_entries: [],
            temp_codeMap: new Map(),
            temp_codeRange: 3,

            searchPile: new Map(),
            discardPile: new Map(),
            potentialPile: new Map(),
            X1: [],
            X2: [],
            X3: [],
            X4: [],
            X5: [],
            confirmedDigits: [],
            isRemove: false,
            isProcessing: false,
            renderCycle: 0,
            processingSize: 100,
            possibilitiesRender: '<span />',
            possibilitiesView: ''
        }
    },
    methods: {
      removeEntry(entry){
        //remove entry
        this.searchPile.delete(entry)

        //retrain app (slow, but safe + reusable for clean start)
        this.discardPile.clear()
        this.potentialPile.clear()

        //this.isRemove = false
        this.X1 = Array.from(defaultPossibilities.baseX)
        this.X2 = Array.from(defaultPossibilities.baseX)
        this.X3 = Array.from(defaultPossibilities.baseX)
        this.X4 = Array.from(defaultPossibilities.baseX)
        this.X5 = Array.from(defaultPossibilities.baseX)
        this.confirmedDigits = Array.from(defaultPossibilities.confirmedDigits)
        this.searchPile.forEach((count, entry)  => {
          this.checkKnown(entry, count, false, true)
        })

        this.rebuildComparitor()
        this.RenderNumbers()
      },
      checkKnown(entry, count, isRemove = false, bypassRebuild = false)
      { 

        if(entry.length < 5) return; //number is invalid

        if(isRemove)
        {
          removeEntry(entry)
          return
        }

        if(!bypassRebuild){
          if(!this.searchPile.has(entry)) {
             this.searchPile.set(entry, count)
          }
          else return
        }
        console.log(entry, count)

        if(Number(count) === 0){
          console.log(entry, count)
          /* Filter step 1.
           * Add entry to pool of discarded numbers. each number in the sequence can be eliminated for the remainder.
           */
          this.discardPile.set(entry, count)
          console.log(this.discardPile)
        } else {
          /* Filter step 2.
           * check and remove entries that knowingly fail x amount of times,
           * do this by checking the current correct numbers in count.
           * if xFailCount + count = 5 digits, remove entry and set confirmedDigits.
           */
          const x = []
          const failsX = []
          let failCount = 0
          let hasNewPotential = false // used for Filter step 3?

          x.length = 5
          failsX.length = 5

          x[0] = (entry[0])
          x[1] = (entry[1])
          x[2] = (entry[2])
          x[3] = (entry[3])
          x[4] = (entry[4])

          //check in sequence the current number in entry X's if values are -1 and add result to failCount
          failCount += (failsX[0] = this.X1[x[0]] === -1) ? 1 : 0
          failCount += (failsX[1] = this.X2[x[1]] === -1) ? 1 : 0
          failCount += (failsX[2] = this.X3[x[2]] === -1) ? 1 : 0
          failCount += (failsX[3] = this.X4[x[3]] === -1) ? 1 : 0
          failCount += (failsX[4] = this.X5[x[4]] === -1) ? 1 : 0

          if(failCount > 0)
          {
            //check if FailCount + count = 5 digits
            if(failCount + count === 5){
              //update and set the correct digits (those that didn't fail)
              for (let i = 0; i < 5; i++)
              {
                if (!failsX[i]) this.confirmedDigits[i] = Number(x[i])
              }
              this.discardPile.set(entry, count)
            } else {
              //variable has known correct values, but positioning is not yet fully known or validated
              this.potentialPile.set(entry, count)
              hasNewPotential = true
            }
          } else {
            //variable has known correct values, but positioning is not yet known or validated
            this.potentialPile.set(entry, count)
            hasNewPotential = true
          }
        }

        if(!bypassRebuild){
          this.rebuildComparitor()
          this.RenderNumbers()
        } 
      },
      rebuildComparitor() {
        this.X1 = Array.from(defaultPossibilities.baseX)
        this.X2 = Array.from(defaultPossibilities.baseX)
        this.X3 = Array.from(defaultPossibilities.baseX)
        this.X4 = Array.from(defaultPossibilities.baseX)
        this.X5 = Array.from(defaultPossibilities.baseX)

        //disable values from X using filter step 1.
        this.discardPile.forEach((count, entry) => {
          if(count === 0){
            let x1 = entry[0]
            let x2 = entry[1]
            let x3 = entry[2]
            let x4 = entry[3]
            let x5 = entry[4]

            this.X1[x1] = -1
            this.X2[x2] = -1
            this.X3[x3] = -1
            this.X4[x4] = -1
            this.X5[x5] = -1
          }
        })

        // skip already confirmed digits
        if (this.confirmedDigits[0] === -1) this.confirmedDigits[0] = this.CheckRemainder(this.X1)
        if (this.confirmedDigits[1] === -1) this.confirmedDigits[1] = this.CheckRemainder(this.X2)
        if (this.confirmedDigits[2] === -1) this.confirmedDigits[2] = this.CheckRemainder(this.X3)
        if (this.confirmedDigits[3] === -1) this.confirmedDigits[3] = this.CheckRemainder(this.X4)
        if (this.confirmedDigits[4] === -1) this.confirmedDigits[4] = this.CheckRemainder(this.X5)

        //disable values from X using filter step 2.
        for (let i = 0; i < 10; i++)
        {
          //TODO: fix rem issue (store state?)
          if (this.confirmedDigits[0] !== -1) this.X1[i] = (this.confirmedDigits[0] === i) ? 0 : -1;
          if (this.confirmedDigits[1] !== -1) this.X2[i] = (this.confirmedDigits[1] === i) ? 0 : -1;
          if (this.confirmedDigits[2] !== -1) this.X3[i] = (this.confirmedDigits[2] === i) ? 0 : -1;
          if (this.confirmedDigits[3] !== -1) this.X4[i] = (this.confirmedDigits[3] === i) ? 0 : -1;
          if (this.confirmedDigits[4] !== -1) this.X5[i] = (this.confirmedDigits[4] === i) ? 0 : -1;
        }
      },
      CheckRemainder(xNUM) {
        //check if all values have been disabled but 1 for X.
        let remainder = 0;
        let checkVal = 0;
        for (let i = 0; i < xNUM.Length; i++)
        {
          if (xNUM[i] != -1) checkVal = i;
          remainder = remainder - xNUM[i];
        }

        return remainder == 9 ? checkVal : -1;
      },
      RenderNumbers(){
        const renderCycle = this.renderCycle + 1
        this.renderCycle = renderCycle

        this.$worker.run((X1, X2, X3, X4, X5, renderCycle) => {
          let retHTML = ''
          for (let i = 0; i < 100000; i++)
          {
            const state = (
                X1[(i % 100000 - i % 10000) / 10000] == -1 ||
                X2[(i % 10000 - i % 1000) / 1000] == -1 ||
                X3[(i % 1000 - i % 100) / 100] == -1 ||
                X4[(i % 100 - i % 10) / 10] == -1 ||
                X5[(i % 10)] == -1
              );

            if (state === false)
            {
              retHTML += `${i.toString().padStart(5, '0')}\t`
            }
          }
          return { html: retHTML, cycle: renderCycle }
        }, [this.X1, this.X2, this.X3, this.X4, this.X5, renderCycle])
        .then(result => {
          if(this.renderCycle == result.cycle){
            this.possibilitiesView = result.html
            this.isProcessing = false
          }
        })
        .catch(e => {
          if(this.renderCycle == result.cycle){
            this.possibilitiesView = e
            this.isProcessing = false
          }
        })
      }
    },
    watch: {
      possibilitiesView(newPossibilitiesView, oldPossibilitiesView)
      {
        /*
        var renderStr = newPossibilitiesView.toString()
        renderStr = renderStr.replaceAll(',', `</span><span class="pill">`)
        renderStr = `<span class="pill">${renderStr}</span>`
        this.possibilitiesRender = renderStr*/
        this.possibilitiesRender = newPossibilitiesView
      }
    },
    computed: {
    },
    async mounted() {
        this.temp_entries = [
            { numbers: '00000', numCorrect: 0 },
            { numbers: '00011', numCorrect: 2 },
            { numbers: '11100', numCorrect: 3 }
        ]

        //Reset
        const correctCode = defaultPossibilities.debugResult
        this.X1 = Array.from(defaultPossibilities.baseX)
        this.X2 = Array.from(defaultPossibilities.baseX)
        this.X3 = Array.from(defaultPossibilities.baseX)
        this.X4 = Array.from(defaultPossibilities.baseX)
        this.X5 = Array.from(defaultPossibilities.baseX)
        this.confirmedDigits = Array.from(defaultPossibilities.confirmedDigits)

        this.isProcessing = true;

        //debug code
        if(this.debug)
        {
          for(let iEntry = 0; iEntry < this.temp_codeRange; iEntry++)
          {
            this.checkKnown(this.temp_entries[iEntry].numbers, this.temp_entries[iEntry].numCorrect)
          }
        }

        if(!this.debug) this.RenderNumbers()
    }
}
</script>

<style>
.test-entries, .possibilities{
  height:40vh;
  overflow-y:auto;
  display: block;
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