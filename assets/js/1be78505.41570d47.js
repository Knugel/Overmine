"use strict";(self.webpackChunkovermine=self.webpackChunkovermine||[]).push([[514],{7994:function(e,t,a){a.r(t),a.d(t,{default:function(){return xe}});var n=a(7294),r=a(6010),l=a(8425),i=a(3320),c=a(1944),o=a(5281),s=a(4477),d=a(1116),m=a(5257),u=a(5999),b=a(2466),v=a(5936);var p="backToTopButton_sjWU",h="backToTopButtonShow_xfvO";function f(){var e=function(e){var t=e.threshold,a=(0,n.useState)(!1),r=a[0],l=a[1],i=(0,n.useRef)(!1),c=(0,b.Ct)(),o=c.startScroll,s=c.cancelScroll;return(0,b.RF)((function(e,a){var n=e.scrollY,r=null==a?void 0:a.scrollY;r&&(i.current?i.current=!1:n>=r?(s(),l(!1)):n<t?l(!1):n+window.innerHeight<document.documentElement.scrollHeight&&l(!0))})),(0,v.S)((function(e){e.location.hash&&(i.current=!0,l(!1))})),{shown:r,scrollToTop:function(){return o(0)}}}({threshold:300}),t=e.shown,a=e.scrollToTop;return n.createElement("button",{"aria-label":(0,u.I)({id:"theme.BackToTopButton.buttonAriaLabel",message:"Scroll back to top",description:"The ARIA label for the back to top button"}),className:(0,r.Z)("clean-btn",o.k.common.backToTopButton,p,t&&h),type:"button",onClick:a})}var E=a(6775),_=a(7524),k=a(6668),g=a(1327),C=a(7462);function I(e){return n.createElement("svg",(0,C.Z)({width:"20",height:"20","aria-hidden":"true"},e),n.createElement("g",{fill:"#7a7a7a"},n.createElement("path",{d:"M9.992 10.023c0 .2-.062.399-.172.547l-4.996 7.492a.982.982 0 01-.828.454H1c-.55 0-1-.453-1-1 0-.2.059-.403.168-.551l4.629-6.942L.168 3.078A.939.939 0 010 2.528c0-.548.45-.997 1-.997h2.996c.352 0 .649.18.828.45L9.82 9.472c.11.148.172.347.172.55zm0 0"}),n.createElement("path",{d:"M19.98 10.023c0 .2-.058.399-.168.547l-4.996 7.492a.987.987 0 01-.828.454h-3c-.547 0-.996-.453-.996-1 0-.2.059-.403.168-.551l4.625-6.942-4.625-6.945a.939.939 0 01-.168-.55 1 1 0 01.996-.997h3c.348 0 .649.18.828.45l4.996 7.492c.11.148.168.347.168.55zm0 0"})))}var S="collapseSidebarButton_PEFL",Z="collapseSidebarButtonIcon_kv0_";function N(e){var t=e.onClick;return n.createElement("button",{type:"button",title:(0,u.I)({id:"theme.docs.sidebar.collapseButtonTitle",message:"Collapse sidebar",description:"The title attribute for collapse button of doc sidebar"}),"aria-label":(0,u.I)({id:"theme.docs.sidebar.collapseButtonAriaLabel",message:"Collapse sidebar",description:"The title attribute for collapse button of doc sidebar"}),className:(0,r.Z)("button button--secondary button--outline",S),onClick:t},n.createElement(I,{className:Z}))}var x=a(9689),y=a(3366),T=a(9688),L=Symbol("EmptyContext"),M=n.createContext(L);function A(e){var t=e.children,a=(0,n.useState)(null),r=a[0],l=a[1],i=(0,n.useMemo)((function(){return{expandedItem:r,setExpandedItem:l}}),[r]);return n.createElement(M.Provider,{value:i},t)}var B=a(8596),w=a(6043),H=a(9960),P=a(2389),F=["item","onItemClick","activePath","level","index"];function W(e){var t=e.categoryLabel,a=e.onClick;return n.createElement("button",{"aria-label":(0,u.I)({id:"theme.DocSidebarItem.toggleCollapsedCategoryAriaLabel",message:"Toggle the collapsible sidebar category '{label}'",description:"The ARIA label to toggle the collapsible sidebar category"},{label:t}),type:"button",className:"clean-btn menu__caret",onClick:a})}function D(e){var t=e.item,a=e.onItemClick,i=e.activePath,c=e.level,s=e.index,d=(0,y.Z)(e,F),m=t.items,u=t.label,b=t.collapsible,v=t.className,p=t.href,h=(0,k.L)().docs.sidebar.autoCollapseCategories,f=function(e){var t=(0,P.Z)();return(0,n.useMemo)((function(){return e.href?e.href:!t&&e.collapsible?(0,l.Wl)(e):void 0}),[e,t])}(t),E=(0,l._F)(t,i),_=(0,B.Mg)(p,i),g=(0,w.u)({initialState:function(){return!!b&&(!E&&t.collapsed)}}),I=g.collapsed,S=g.setCollapsed,Z=function(){var e=(0,n.useContext)(M);if(e===L)throw new T.i6("DocSidebarItemsExpandedStateProvider");return e}(),N=Z.expandedItem,x=Z.setExpandedItem,A=function(e){void 0===e&&(e=!I),x(e?null:s),S(e)};return function(e){var t=e.isActive,a=e.collapsed,r=e.updateCollapsed,l=(0,T.D9)(t);(0,n.useEffect)((function(){t&&!l&&a&&r(!1)}),[t,l,a,r])}({isActive:E,collapsed:I,updateCollapsed:A}),(0,n.useEffect)((function(){b&&N&&N!==s&&h&&S(!0)}),[b,N,s,S,h]),n.createElement("li",{className:(0,r.Z)(o.k.docs.docSidebarItemCategory,o.k.docs.docSidebarItemCategoryLevel(c),"menu__list-item",{"menu__list-item--collapsed":I},v)},n.createElement("div",{className:(0,r.Z)("menu__list-item-collapsible",{"menu__list-item-collapsible--active":_})},n.createElement(H.Z,(0,C.Z)({className:(0,r.Z)("menu__link",{"menu__link--sublist":b,"menu__link--sublist-caret":!p&&b,"menu__link--active":E}),onClick:b?function(e){null==a||a(t),p?A(!1):(e.preventDefault(),A())}:function(){null==a||a(t)},"aria-current":_?"page":void 0,"aria-expanded":b?!I:void 0,href:b?null!=f?f:"#":f},d),u),p&&b&&n.createElement(W,{categoryLabel:u,onClick:function(e){e.preventDefault(),A()}})),n.createElement(w.z,{lazy:!0,as:"ul",className:"menu__list",collapsed:I},n.createElement(J,{items:m,tabIndex:I?-1:0,onItemClick:a,activePath:i,level:c+1})))}var R=a(3919),z=a(8483),K="menuExternalLink_NmtK",V=["item","onItemClick","activePath","level","index"];function j(e){var t=e.item,a=e.onItemClick,i=e.activePath,c=e.level,s=(e.index,(0,y.Z)(e,V)),d=t.href,m=t.label,u=t.className,b=(0,l._F)(t,i),v=(0,R.Z)(d);return n.createElement("li",{className:(0,r.Z)(o.k.docs.docSidebarItemLink,o.k.docs.docSidebarItemLinkLevel(c),"menu__list-item",u),key:m},n.createElement(H.Z,(0,C.Z)({className:(0,r.Z)("menu__link",!v&&K,{"menu__link--active":b}),"aria-current":b?"page":void 0,to:d},v&&{onClick:a?function(){return a(t)}:void 0},s),m,!v&&n.createElement(z.Z,null)))}var G="menuHtmlItem_M9Kj";function U(e){var t=e.item,a=e.level,l=e.index,i=t.value,c=t.defaultStyle,s=t.className;return n.createElement("li",{className:(0,r.Z)(o.k.docs.docSidebarItemLink,o.k.docs.docSidebarItemLinkLevel(a),c&&[G,"menu__list-item"],s),key:l,dangerouslySetInnerHTML:{__html:i}})}var Y=["item"];function q(e){var t=e.item,a=(0,y.Z)(e,Y);switch(t.type){case"category":return n.createElement(D,(0,C.Z)({item:t},a));case"html":return n.createElement(U,(0,C.Z)({item:t},a));default:return n.createElement(j,(0,C.Z)({item:t},a))}}var O=["items"];function X(e){var t=e.items,a=(0,y.Z)(e,O);return n.createElement(A,null,t.map((function(e,t){return n.createElement(q,(0,C.Z)({key:t,item:e,index:t},a))})))}var J=(0,n.memo)(X),Q="menu_SIkG",$="menuWithAnnouncementBar_GW3s";function ee(e){var t=e.path,a=e.sidebar,l=e.className,i=function(){var e=(0,x.nT)().isActive,t=(0,n.useState)(e),a=t[0],r=t[1];return(0,b.RF)((function(t){var a=t.scrollY;e&&r(0===a)}),[e]),e&&a}();return n.createElement("nav",{className:(0,r.Z)("menu thin-scrollbar",Q,i&&$,l)},n.createElement("ul",{className:(0,r.Z)(o.k.docs.docSidebarMenu,"menu__list")},n.createElement(J,{items:a,activePath:t,level:1})))}var te="sidebar_njMd",ae="sidebarWithHideableNavbar_wUlq",ne="sidebarHidden_VK0M",re="sidebarLogo_isFc";function le(e){var t=e.path,a=e.sidebar,l=e.onCollapse,i=e.isHidden,c=(0,k.L)(),o=c.navbar.hideOnScroll,s=c.docs.sidebar.hideable;return n.createElement("div",{className:(0,r.Z)(te,o&&ae,i&&ne)},o&&n.createElement(g.Z,{tabIndex:-1,className:re}),n.createElement(ee,{path:t,sidebar:a}),s&&n.createElement(N,{onClick:l}))}var ie=n.memo(le),ce=a(2961),oe=a(3102),se=function(e){var t=e.sidebar,a=e.path,l=(0,ce.e)();return n.createElement("ul",{className:(0,r.Z)(o.k.docs.docSidebarMenu,"menu__list")},n.createElement(J,{items:t,activePath:a,onItemClick:function(e){"category"===e.type&&e.href&&l.toggle(),"link"===e.type&&l.toggle()},level:1}))};function de(e){return n.createElement(oe.Zo,{component:se,props:e})}var me=n.memo(de);function ue(e){var t=(0,_.i)(),a="desktop"===t||"ssr"===t,r="mobile"===t;return n.createElement(n.Fragment,null,a&&n.createElement(ie,e),r&&n.createElement(me,e))}var be="expandButton_m80_",ve="expandButtonIcon_BlDH";function pe(e){var t=e.toggleSidebar;return n.createElement("div",{className:be,title:(0,u.I)({id:"theme.docs.sidebar.expandButtonTitle",message:"Expand sidebar",description:"The ARIA label and title attribute for expand button of doc sidebar"}),"aria-label":(0,u.I)({id:"theme.docs.sidebar.expandButtonAriaLabel",message:"Expand sidebar",description:"The ARIA label and title attribute for expand button of doc sidebar"}),tabIndex:0,role:"button",onKeyDown:t,onClick:t},n.createElement(I,{className:ve}))}var he="docSidebarContainer_b6E3",fe="docSidebarContainerHidden_b3ry";function Ee(e){var t,a=e.children,r=(0,d.V)();return n.createElement(n.Fragment,{key:null!=(t=null==r?void 0:r.name)?t:"noSidebar"},a)}function _e(e){var t=e.sidebar,a=e.hiddenSidebarContainer,l=e.setHiddenSidebarContainer,i=(0,E.TH)().pathname,c=(0,n.useState)(!1),s=c[0],d=c[1],m=(0,n.useCallback)((function(){s&&d(!1),l((function(e){return!e}))}),[l,s]);return n.createElement("aside",{className:(0,r.Z)(o.k.docs.docSidebarContainer,he,a&&fe),onTransitionEnd:function(e){e.currentTarget.classList.contains(he)&&a&&d(!0)}},n.createElement(Ee,null,n.createElement(ue,{sidebar:t,path:i,onCollapse:m,isHidden:s})),s&&n.createElement(pe,{toggleSidebar:m}))}var ke={docMainContainer:"docMainContainer_gTbr",docMainContainerEnhanced:"docMainContainerEnhanced_Uz_u",docItemWrapperEnhanced:"docItemWrapperEnhanced_czyv"};function ge(e){var t=e.hiddenSidebarContainer,a=e.children,l=(0,d.V)();return n.createElement("main",{className:(0,r.Z)(ke.docMainContainer,(t||!l)&&ke.docMainContainerEnhanced)},n.createElement("div",{className:(0,r.Z)("container padding-top--md padding-bottom--lg",ke.docItemWrapper,t&&ke.docItemWrapperEnhanced)},a))}var Ce="docPage__5DB",Ie="docsWrapper_BCFX";function Se(e){var t=e.children,a=(0,d.V)(),r=(0,n.useState)(!1),l=r[0],i=r[1];return n.createElement(m.Z,{wrapperClassName:Ie},n.createElement(f,null),n.createElement("div",{className:Ce},a&&n.createElement(_e,{sidebar:a.items,hiddenSidebarContainer:l,setHiddenSidebarContainer:i}),n.createElement(ge,{hiddenSidebarContainer:l},t)))}var Ze=a(4972),Ne=a(197);function xe(e){var t=e.versionMetadata,a=(0,l.hI)(e);if(!a)return n.createElement(Ze.default,null);var m=a.docElement,u=a.sidebarName,b=a.sidebarItems;return n.createElement(n.Fragment,null,n.createElement(Ne.Z,{version:t.version,tag:(0,i.os)(t.pluginId,t.version)}),n.createElement(c.FG,{className:(0,r.Z)(o.k.wrapper.docsPages,o.k.page.docsDocPage,e.versionMetadata.className)},n.createElement(s.q,{version:t},n.createElement(d.b,{name:u,items:b},n.createElement(Se,null,m)))))}}}]);