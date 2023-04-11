(function(){"use strict";var e={335:function(e,t,i){var s=i(144),r=i(998),o=i(6072),n=i(6570),c=i(4324),a=i(3059),l=i(3687),d=function(){var e=this,t=e._self._c;return t(r.Z,[t(o.Z,{attrs:{app:"",color:"primary",dark:""}},[t("div",{staticClass:"d-flex align-center"},[t("span",{staticClass:"shrink mt-1 hidden-sm-and-down"},[e._v('538\'s "CODE = CASHEN" Cracker')])]),t(l.Z),t(n.Z,{attrs:{href:"https://github.com/KuroPSPiso/decode-that-mastermind",target:"_blank",text:""}},[t("span",{staticClass:"mr-2"},[e._v("Latest Release")]),t(c.Z,[e._v("mdi-open-in-new")])],1)],1),t(a.Z,[t("CodeEntries")],1)],1)},h=[],u=i(4127),f=i(9043),m=i(5125),p=i(6760),g=i(7567),b=i(1466),v=function(){var e=this,t=e._self._c;return t("div",[t("div",{staticClass:"entries"},[t(p.Z,[t("div",{staticClass:"px-4 pt-2"},[t(g.Z,{staticClass:"mx-auto",attrs:{width:"300"}},[t(m.Z,{ref:"entryForm"},[t(b.Z,{attrs:{label:"Code",rules:e.codeRules},model:{value:e.code,callback:function(t){e.code=t},expression:"code"}}),t(b.Z,{attrs:{label:"Count",rules:e.countRules,type:"number"},model:{value:e.count,callback:function(t){e.count=t},expression:"count"}}),t(n.Z,{attrs:{color:"primary"},on:{click:e.addRecord}},[e._v("Add")])],1)],1)],1),t(f.Z,e._l(e.codeRecords,(function(i){return t(u.Z,{key:i.key,attrs:{color:"primary"},on:{click:function(t){return e.remove(i[0],i[1])}}},[e._v(e._s(i[0])+" ("+e._s(i[1])+") "),t(c.Z,[e._v("mdi-delete-outline")])],1)})),1)],1)],1),t("Possibilites",{ref:"possibilities"})],1)},y=[],k=function(){var e=this,t=e._self._c;return t("div",[e.debug?t("div",{staticClass:"test-entries"},e._l(e.temp_entries,(function(i){return t("p",{key:i.numbers},[e._v(e._s(i.numbers)+" ("+e._s(i.numCorrect)+")")])})),0):e._e(),e.isProcessing?t("div",[e._v(" processing possibilities... ")]):t("code",{staticClass:"possibilities",domProps:{innerHTML:e._s(e.possibilitiesRender)}})])},X=[],w=JSON.parse('{"kU":[0,0,0,0,0,0,0,0,0,0],"VJ":[-1,-1,-1,-1,-1],"pB":"11111"}'),P={name:"Possibilities",data(){return{debug:!1,temp_entries:[],temp_codeMap:new Map,temp_codeRange:3,searchPile:new Map,discardPile:new Map,potentialPile:new Map,X1:[],X2:[],X3:[],X4:[],X5:[],confirmedDigits:[],isRemove:!1,isProcessing:!1,renderCycle:0,processingSize:100,possibilitiesRender:"<span />",possibilitiesView:""}},methods:{removeEntry(e){this.searchPile.delete(e),this.discardPile.clear(),this.potentialPile.clear(),this.X1=Array.from(w.kU),this.X2=Array.from(w.kU),this.X3=Array.from(w.kU),this.X4=Array.from(w.kU),this.X5=Array.from(w.kU),this.confirmedDigits=Array.from(w.VJ),this.searchPile.forEach(((e,t)=>{this.checkKnown(t,e,!1,!0)})),this.rebuildComparitor(),this.RenderNumbers()},checkKnown(e,t,i=!1,s=!1){if(!(e.length<5))if(i)removeEntry(e);else{if(!s){if(this.searchPile.has(e))return;this.searchPile.set(e,t)}if(console.log(e,t),0===Number(t))console.log(e,t),this.discardPile.set(e,t),console.log(this.discardPile);else{const i=[],s=[];let r=0,o=!1;if(i.length=5,s.length=5,i[0]=e[0],i[1]=e[1],i[2]=e[2],i[3]=e[3],i[4]=e[4],r+=(s[0]=-1===this.X1[i[0]])?1:0,r+=(s[1]=-1===this.X2[i[1]])?1:0,r+=(s[2]=-1===this.X3[i[2]])?1:0,r+=(s[3]=-1===this.X4[i[3]])?1:0,r+=(s[4]=-1===this.X5[i[4]])?1:0,r>0)if(r+t===5){for(let e=0;e<5;e++)s[e]||(this.confirmedDigits[e]=Number(i[e]));this.discardPile.set(e,t)}else this.potentialPile.set(e,t),o=!0;else this.potentialPile.set(e,t),o=!0}s||(this.rebuildComparitor(),this.RenderNumbers())}},rebuildComparitor(){this.X1=Array.from(w.kU),this.X2=Array.from(w.kU),this.X3=Array.from(w.kU),this.X4=Array.from(w.kU),this.X5=Array.from(w.kU),this.discardPile.forEach(((e,t)=>{if(0===e){let e=t[0],i=t[1],s=t[2],r=t[3],o=t[4];this.X1[e]=-1,this.X2[i]=-1,this.X3[s]=-1,this.X4[r]=-1,this.X5[o]=-1}})),-1===this.confirmedDigits[0]&&(this.confirmedDigits[0]=this.CheckRemainder(this.X1)),-1===this.confirmedDigits[1]&&(this.confirmedDigits[1]=this.CheckRemainder(this.X2)),-1===this.confirmedDigits[2]&&(this.confirmedDigits[2]=this.CheckRemainder(this.X3)),-1===this.confirmedDigits[3]&&(this.confirmedDigits[3]=this.CheckRemainder(this.X4)),-1===this.confirmedDigits[4]&&(this.confirmedDigits[4]=this.CheckRemainder(this.X5));for(let e=0;e<10;e++)-1!==this.confirmedDigits[0]&&(this.X1[e]=this.confirmedDigits[0]===e?0:-1),-1!==this.confirmedDigits[1]&&(this.X2[e]=this.confirmedDigits[1]===e?0:-1),-1!==this.confirmedDigits[2]&&(this.X3[e]=this.confirmedDigits[2]===e?0:-1),-1!==this.confirmedDigits[3]&&(this.X4[e]=this.confirmedDigits[3]===e?0:-1),-1!==this.confirmedDigits[4]&&(this.X5[e]=this.confirmedDigits[4]===e?0:-1)},CheckRemainder(e){let t=0,i=0;for(let s=0;s<e.Length;s++)-1!=e[s]&&(i=s),t-=e[s];return 9==t?i:-1},RenderNumbers(){const e=this.renderCycle+1;this.renderCycle=e,this.$worker.run(((e,t,i,s,r,o)=>{let n="";for(let c=0;c<1e5;c++){const o=-1==e[(c%1e5-c%1e4)/1e4]||-1==t[(c%1e4-c%1e3)/1e3]||-1==i[(c%1e3-c%100)/100]||-1==s[(c%100-c%10)/10]||-1==r[c%10];!1===o&&(n+=`${c.toString().padStart(5,"0")}\t`)}return{html:n,cycle:o}}),[this.X1,this.X2,this.X3,this.X4,this.X5,e]).then((e=>{this.renderCycle==e.cycle&&(this.possibilitiesView=e.html,this.isProcessing=!1)})).catch((e=>{this.renderCycle==result.cycle&&(this.possibilitiesView=e,this.isProcessing=!1)}))}},watch:{possibilitiesView(e,t){this.possibilitiesRender=e}},computed:{},async mounted(){this.temp_entries=[{numbers:"00000",numCorrect:0},{numbers:"00011",numCorrect:2},{numbers:"11100",numCorrect:3}];w.pB;if(this.X1=Array.from(w.kU),this.X2=Array.from(w.kU),this.X3=Array.from(w.kU),this.X4=Array.from(w.kU),this.X5=Array.from(w.kU),this.confirmedDigits=Array.from(w.VJ),this.isProcessing=!0,this.debug)for(let e=0;e<this.temp_codeRange;e++)this.checkKnown(this.temp_entries[e].numbers,this.temp_entries[e].numCorrect);this.debug||this.RenderNumbers()}},C=P,_=i(1001),R=(0,_.Z)(C,k,X,!1,null,null,null),D=R.exports,Z={name:"CodeEntries",components:{Possibilites:D},data(){return{codeRecords:new Map,code:"",count:"",codeRules:[e=>!!e||"A 5 number Code is required",e=>e&&5===e.length||"The code must consist out of 5 numbers"],countRules:[e=>!!e||"A amount of correct numbers is Required",e=>e&&e>=0||"The count must be higher or equal to 0",e=>e&&e<=5||"The count must be lower or equal to 5"]}},methods:{saveData(){if(null!==this.codeRecords.entries()){const e=JSON.stringify(Object.fromEntries(this.codeRecords));localStorage.setItem("codes",e)}else localStorage.removeItem("codes")},loadData(){null!==localStorage.getItem("codes")&&(this.codeRecords=new Map(Object.entries(JSON.parse(localStorage.getItem("codes"))))),this.codeRecords.forEach(((e,t)=>{this.$refs.possibilities.isProcessing=!0,this.$refs.possibilities.checkKnown(t.toString(),Number(e))}))},async addRecord(){const e=await this.$refs.entryForm.validate();e&&(this.codeRecords.has(this.code)?this.count!==this.codeRecords.get(this.code)&&alert(`You already added ${this.code} before with a different count.`):(this.codeRecords.set(this.code.toString(),Number(this.count)),this.saveData(),this.$refs.possibilities.isProcessing=!0,this.$refs.possibilities.checkKnown(this.code.toString(),Number(this.count)),this.$forceUpdate()))},remove(e,t){confirm(`Remove code "${e}" with ${Number(t)} correct numbers`)&&(this.codeRecords.delete(e.toString()),this.saveData(),this.$refs.possibilities.isProcessing=!0,this.$refs.possibilities.removeEntry(e.toString()),this.$forceUpdate())}},mounted(){this.loadData()}},A=Z,S=(0,_.Z)(A,v,y,!1,null,null,null),O=S.exports,U={name:"App",components:{CodeEntries:O},data:()=>({})},N=U,$=(0,_.Z)(N,d,h,!1,null,null,null),E=$.exports,x=i(5205);(0,x.z)("/decode-that-mastermind/service-worker.js",{ready(){console.log("App is being served from cache by a service worker.\nFor more details, visit https://goo.gl/AFskqB")},registered(){console.log("Service worker has been registered.")},cached(){console.log("Content has been cached for offline use.")},updatefound(){console.log("New content is downloading.")},updated(){console.log("New content is available; please refresh.")},offline(){console.log("No internet connection found. App is running in offline mode.")},error(e){console.error("Error during service worker registration:",e)}});var j=i(629);s.ZP.use(j.ZP);var M=new j.ZP.Store({state:{},getters:{},mutations:{},actions:{},modules:{}}),T=i(8864);s.ZP.use(T.Z);var V=new T.Z({icons:{iconfont:"mdi"}}),J=i(9556);s.ZP.config.productionTip=!1,s.ZP.use(J.Z),new s.ZP({store:M,vuetify:V,render:e=>e(E)}).$mount("#app")}},t={};function i(s){var r=t[s];if(void 0!==r)return r.exports;var o=t[s]={exports:{}};return e[s](o,o.exports,i),o.exports}i.m=e,function(){var e=[];i.O=function(t,s,r,o){if(!s){var n=1/0;for(d=0;d<e.length;d++){s=e[d][0],r=e[d][1],o=e[d][2];for(var c=!0,a=0;a<s.length;a++)(!1&o||n>=o)&&Object.keys(i.O).every((function(e){return i.O[e](s[a])}))?s.splice(a--,1):(c=!1,o<n&&(n=o));if(c){e.splice(d--,1);var l=r();void 0!==l&&(t=l)}}return t}o=o||0;for(var d=e.length;d>0&&e[d-1][2]>o;d--)e[d]=e[d-1];e[d]=[s,r,o]}}(),function(){i.n=function(e){var t=e&&e.__esModule?function(){return e["default"]}:function(){return e};return i.d(t,{a:t}),t}}(),function(){i.d=function(e,t){for(var s in t)i.o(t,s)&&!i.o(e,s)&&Object.defineProperty(e,s,{enumerable:!0,get:t[s]})}}(),function(){i.g=function(){if("object"===typeof globalThis)return globalThis;try{return this||new Function("return this")()}catch(e){if("object"===typeof window)return window}}()}(),function(){i.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)}}(),function(){i.r=function(e){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})}}(),function(){var e={143:0};i.O.j=function(t){return 0===e[t]};var t=function(t,s){var r,o,n=s[0],c=s[1],a=s[2],l=0;if(n.some((function(t){return 0!==e[t]}))){for(r in c)i.o(c,r)&&(i.m[r]=c[r]);if(a)var d=a(i)}for(t&&t(s);l<n.length;l++)o=n[l],i.o(e,o)&&e[o]&&e[o][0](),e[o]=0;return i.O(d)},s=self["webpackChunkdecode_that_mastermind"]=self["webpackChunkdecode_that_mastermind"]||[];s.forEach(t.bind(null,0)),s.push=t.bind(null,s.push.bind(s))}();var s=i.O(void 0,[998],(function(){return i(335)}));s=i.O(s)})();
//# sourceMappingURL=app.4fd5bf68.js.map