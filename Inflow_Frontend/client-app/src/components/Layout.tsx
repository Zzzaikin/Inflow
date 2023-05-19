import React, {RefObject} from 'react';
import './Layout.scss'

export let LAYOUT_REF: RefObject<any>;

function Layout() {
    LAYOUT_REF = React.createRef();

    return (
        <div ref={LAYOUT_REF} className="layout"></div>
    );
}

export default Layout;