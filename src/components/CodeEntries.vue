<template>
  <div>
    <div class="entries">
      <v-layout>
        <div class="px-4 pt-2">
          <v-sheet width="300" class="mx-auto">
              <v-form ref="entryForm">
                  <v-text-field label="Code" v-model="code" :rules="codeRules"></v-text-field>
                  <v-text-field label="Count" v-model="count" :rules="countRules" type="number"></v-text-field>
                  <v-btn color="primary" @click="addRecord">Add</v-btn>
              </v-form>
          </v-sheet>
        </div>
        <v-chip-group>
            <v-chip color="primary" v-for="entry in codeRecords" :key="entry.key" @click="remove(entry[0], entry[1])">{{entry[0]}} ({{entry[1]}}) <v-icon>mdi-delete-outline</v-icon></v-chip>
        </v-chip-group>
      </v-layout>
    </div>
    <Possibilites ref="possibilities" />
  </div>
</template>

<script>
import Possibilites from './Possibilites.vue'

export default {
    name: 'CodeEntries',
    components: {
      Possibilites
    },
    data() {
      return {
        codeRecords: new Map(),
        code: '',
        count: '',

        codeRules: [
          v => !!v || 'A 5 number Code is required',
          v => (v && v.length === 5) || 'The code must consist out of 5 numbers',
        ],
        countRules: [
          v => !!v || "A amount of correct numbers is Required",
          v => (v && v >= 0) || "The count must be higher or equal to 0",
          v => (v && v <= 5) || "The count must be lower or equal to 5"
        ]
      }
    },
    methods: {
      saveData() {
        if(this.codeRecords.entries() !== null) {
          const jsonMap = JSON.stringify(Object.fromEntries(this.codeRecords))
          localStorage.setItem('codes', jsonMap)
        } else {
          localStorage.removeItem('codes')
        }
      },
      loadData() {
        if(localStorage.getItem('codes')!== null){
          this.codeRecords = new Map(Object.entries(JSON.parse(localStorage.getItem('codes'))))
        }
        this.codeRecords.forEach((count, code) => {
            this.$refs.possibilities.isProcessing = true
            this.$refs.possibilities.checkKnown(code.toString(), Number(count))
        });
      },
      async addRecord() {
        const valid = await this.$refs.entryForm.validate()
        if(valid){
          if(this.codeRecords.has(this.code)) { 
            if(this.count !== this.codeRecords.get(this.code)){
                alert(`You already added ${this.code} before with a different count.`)
            }
          }
          else { 
            this.codeRecords.set(this.code.toString(), Number(this.count))
            this.saveData()

            this.$refs.possibilities.isProcessing = true
            this.$refs.possibilities.checkKnown(this.code.toString(), Number(this.count))

            this.$forceUpdate()
          }
        }
      },
      remove(code, count){
        if (confirm(`Remove code "${code}" with ${Number(count)} correct numbers`)){
          this.codeRecords.delete(code.toString())
          this.saveData()

          this.$refs.possibilities.isProcessing = true
          this.$refs.possibilities.removeEntry(code.toString())
          
          this.$forceUpdate()
        }
      }
    },
    mounted() {
      this.loadData()
    }
}
</script>

<style>

.entries{
  border-bottom: 0.2rem solid blue;
  padding: 1rem;
}

</style>