import React, {RefObject} from 'react';
import './Layout.scss'

export const LAYOUT_REF: RefObject<any> = React.createRef();

function Layout() {

    return (
        <div ref={LAYOUT_REF} className="layout"></div>
    );
}

export default Layout;