"use strict";(self.webpackChunkovermine=self.webpackChunkovermine||[]).push([[817],{4228:function(e,t,n){n.r(t),n.d(t,{default:function(){return D}});var r=n(7294),a=n(1944),i=n(8425),c=n(4996),l=n(6010),o=n(9960),m=n(3919),s=n(5999),u="cardContainer_fWXF",d="cardTitle_rnsV",f="cardDescription_PWke";function E(e){var t=e.href,n=e.children;return r.createElement(o.Z,{href:t,className:(0,l.Z)("card padding--lg",u)},n)}function g(e){var t=e.href,n=e.icon,a=e.title,i=e.description;return r.createElement(E,{href:t},r.createElement("h2",{className:(0,l.Z)("text--truncate",d),title:a},n," ",a),i&&r.createElement("p",{className:(0,l.Z)("text--truncate",f),title:i},i))}function p(e){var t=e.item,n=(0,i.Wl)(t);return n?r.createElement(g,{href:n,icon:"\ud83d\uddc3\ufe0f",title:t.label,description:(0,s.I)({message:"{count} items",id:"theme.docs.DocCard.categoryDescription",description:"The default description for a category card in the generated index about how many items this category includes"},{count:t.items.length})}):null}function h(e){var t,n=e.item,a=(0,m.Z)(n.href)?"\ud83d\udcc4\ufe0f":"\ud83d\udd17",c=(0,i.xz)(null!=(t=n.docId)?t:void 0);return r.createElement(g,{href:n.href,icon:a,title:n.label,description:null==c?void 0:c.description})}function v(e){var t=e.item;switch(t.type){case"link":return r.createElement(h,{item:t});case"category":return r.createElement(p,{item:t});default:throw new Error("unknown item type "+JSON.stringify(t))}}function y(e){var t=e.items,n=e.className;return r.createElement("section",{className:(0,l.Z)("row",n)},function(e){return e.filter((function(e){return"category"!==e.type||!!(0,i.Wl)(e)}))}(t).map((function(e,t){return r.createElement("article",{key:t,className:"col col--6 margin-bottom--lg"},r.createElement(v,{key:t,item:e}))})))}var k=n(4966),N=n(3120),Z=n(4364),w=n(7684),x=n(2503),b="generatedIndexPage_vN6x",I="list_eTzJ",_="title_kItE";function C(e){var t=e.categoryGeneratedIndex;return r.createElement(a.d,{title:t.title,description:t.description,keywords:t.keywords,image:(0,c.Z)(t.image)})}function W(e){var t=e.categoryGeneratedIndex,n=(0,i.jA)();return r.createElement(r.Fragment,null,r.createElement(a.d,{title:t.title,description:t.description,keywords:t.keywords,image:(0,c.Z)(t.image)}),r.createElement("div",{className:b},r.createElement(N.Z,null),r.createElement(w.Z,null),r.createElement(Z.Z,null),r.createElement("header",null,r.createElement(x.Z,{as:"h1",className:_},t.title),t.description&&r.createElement("p",null,t.description)),r.createElement("article",{className:"margin-top--lg"},r.createElement(y,{items:n.items,className:I})),r.createElement("footer",{className:"margin-top--lg"},r.createElement(k.Z,{previous:t.navigation.previous,next:t.navigation.next}))))}function D(e){return r.createElement(r.Fragment,null,r.createElement(C,e),r.createElement(W,e))}}}]);